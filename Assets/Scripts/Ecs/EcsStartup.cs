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

        // Injections
        [SerializeField] private SpawnTable spawnTable;
        [SerializeField] private SceneContext sceneContext;

        /// <summary>
        /// Method for adding updateSystems to ecs world
        /// </summary>
        private void AddSystems()
        {
            updateSystems
                .Add(new UnitStateSystem())
                .Add(new HealthSystem())
                .Add(new PathFollowSystem())

                .Add(new AiSpawnerControlSystem())
                .Add(new UnitSpawnSystem())
                .Add(new UnitInitializationSystem())

                .Add(new SwapTeamSystem())

                .Add(new TargetFindingSystem())
                .Add(new AttackSystem())
                .Add(new MeleeAttackSystem())
                .Add(new RangedAttackSystem())
                
                .Add(new AnimationSystem())
                .Add(new HealthBarUpdateSystem())
                .Add(new CorpsCleanupSystem())
                
                .Add(new TestInitSystem());
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
                .OneFrame<SwapTeamEvent>()
                .OneFrame<HealthChangedEvent>()
                .OneFrame<UnitDeathEvent>()
                .OneFrame<UnitInitializationEvent>()
                .OneFrame<UnitStateChangedEvent>()
                .OneFrame<UpdateAnimationEvent>()
                .OneFrame<SpawnUnitEvent>()
                .OneFrame<AttackEvent>()
                .OneFrame<UninitializedTag>();
        }

        private void Start()
        {
            world = new EcsWorld();
            updateSystems = new EcsSystems(world);

            AddInjections();
            AddSystems();
            AddOneframe();

            updateSystems.Init();
        }

        private void Update()
        {
            updateSystems.Run();
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
