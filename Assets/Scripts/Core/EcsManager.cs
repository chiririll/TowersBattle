using UnityEngine;
using Leopotam.Ecs;
using TowersBattle.Data;
using TowersBattle.Ecs;
using Zenject;

namespace TowersBattle.Core
{
    /// <summary>
    /// Class for starting and running ecs
    /// </summary>
    public class EcsManager : MonoBehaviour 
    {
        private EcsWorld world;
        private EcsSystems updateSystems;
        private EcsSystems persistSystems;

        [Inject] private GameManager gameManager;
        [Inject] private InputManager inputManager;

        [SerializeField] private SpawnTable spawnTable; // TODO: remove
        [SerializeField] private SceneEntityConvertor sceneContext;

        /// <summary>
        /// Event handler for player spawn unit event
        /// </summary>
        private void PlayerSpawnUnit(Unit unit, float coolDown)
        {
            EcsEntity ent = world.NewEntity();
            ref var request = ref ent.Get<PlayerUnitSpawnRequest>();
            request.unit = unit;
        }

        /// <summary>
        /// Method for adding updateSystems to ecs world
        /// </summary>
        private void AddSystems()
        {
            updateSystems
                .Add(new GameStateControlSystem())
                .Add(new UnitStateSystem())
                .Add(new HealthSystem())
                .Add(new PathFollowSystem())

                .Add(new PlayerSpawnerControlSystem())
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
                
                .Add(new TestInitSystem());

            persistSystems
                .Add(new CorpseCleanupSystem());
        }   
        
        /// <summary>
        /// Method for adding injections into components
        /// </summary>
        private void AddInjections()
        {
            updateSystems
                .Inject(gameManager)
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

        private void OnEnable()
        {
            // Subscribing to events
            inputManager.PlayerSpawnUnitEvent += PlayerSpawnUnit;
        }

        private void OnDisable()
        {
            // Unsubscribing from events
            inputManager.PlayerSpawnUnitEvent -= PlayerSpawnUnit;
        }

        private void Start()
        {
            world = new EcsWorld();
            updateSystems = new EcsSystems(world);
            persistSystems = new EcsSystems(world);

            AddInjections();
            AddSystems();
            AddOneframe();

            updateSystems.Init();
            persistSystems.Init();
        }

        private void Update()
        {
            if (gameManager.State == GameState.Playing)
                updateSystems.Run();

            persistSystems.Run();
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
