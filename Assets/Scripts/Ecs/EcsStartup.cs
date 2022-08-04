namespace TowersBattle.Ecs
{
    using UnityEngine;
    using Leopotam.Ecs;
    using TowersBattle.Data;

    /// <summary>
    /// Class for starting and running ecs
    /// </summary>
    public class EcsStartup : MonoBehaviour 
    {
        private EcsWorld world;
        private EcsSystems systems;

        // Injections
        [SerializeField] private SpawnTable spawnTable;
        [SerializeField] private SceneContext sceneContext;

        /// <summary>
        /// Method for adding systems to ecs world
        /// </summary>
        private void AddSystems()
        {
            systems
                .Add(new PathFollowSystem())
                .Add(new AnimationSystem())
                .Add(new TestInitSystem());
        }   
        
        /// <summary>
        /// Method for adding injections into components
        /// </summary>
        private void AddInjections()
        {
            systems
                .Inject(spawnTable)
                .Inject(sceneContext);
        }

        /// <summary>
        /// Method for adding one frame components (e.g. events) to ecs world
        /// </summary>
        private void AddOneframe()
        {
            systems
                .OneFrame<UnitStateChangedEvent>();
        }

        private void Start()
        {
            world = new EcsWorld();
            systems = new EcsSystems(world);

            AddInjections();
            AddSystems();
            AddOneframe();

            systems.Init();
        }

        private void Update()
        {
            systems.Run();
        }

        private void OnDestroy()
        {
            systems?.Destroy();
            systems = null;

            world?.Destroy();
            world = null;
        }
    }
}
