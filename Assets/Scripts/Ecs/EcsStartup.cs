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
        private EcsSystems updateSystems;
        private EcsSystems fixedUpdateSystems;

        // Injections
        [SerializeField] private SpawnTable spawnTable;
        [SerializeField] private SceneContext sceneContext;

        /// <summary>
        /// Method for adding updateSystems to ecs world
        /// </summary>
        private void AddSystems()
        {
            updateSystems
                .Add(new UnitInitializationSystem())
                .Add(new SwapTeamSystem())
                .Add(new PathFollowSystem())
                .Add(new TargetFindingSystem())
                .Add(new AnimationSystem())
                .Add(new TestInitSystem());

            //fixedUpdateSystems
            //    .Add(new TargetFindingSystem());
        }   
        
        /// <summary>
        /// Method for adding injections into components
        /// </summary>
        private void AddInjections()
        {
            updateSystems
                .Inject(spawnTable)
                .Inject(sceneContext);
        }

        /// <summary>
        /// Method for adding one frame components (e.g. events) to ecs world
        /// </summary>
        private void AddOneframe()
        {
            updateSystems
                .OneFrame<UnitInitializationEvent>()
                .OneFrame<UnitStateChangedEvent>()
                .OneFrame<UpdateAnimationEvent>();
        }

        private void Start()
        {
            world = new EcsWorld();
            updateSystems = new EcsSystems(world);
            fixedUpdateSystems = new EcsSystems(world);

            AddInjections();
            AddSystems();
            AddOneframe();

            updateSystems.Init();
            fixedUpdateSystems.Init();
        }

        private void Update()
        {
            updateSystems.Run();
        }

        private void FixedUpdate()
        {
            fixedUpdateSystems.Run();
        }

        private void OnDestroy()
        {
            updateSystems?.Destroy();
            updateSystems = null;

            world?.Destroy();
            world = null;
        }
    }
}
