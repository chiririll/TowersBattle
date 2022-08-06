namespace TowersBattle.Ecs
{
    using Leopotam.Ecs;
    using TowersBattle.Data;
    using UnityEngine;

    /// <summary>
    /// TODO
    /// </summary>
    public class UnitInitializationSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld ecsWorld;
        private SceneContext scene;

        private EcsFilter<UnitInitializationEvent> filter;

        public void Init()
        {
            foreach (UnitObjectData unitData in scene.units)
            {
                EcsEntity ent = ecsWorld.NewEntity();

                ConvertGameObject(ref ent, unitData);
            }
        }

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var ent = ref filter.GetEntity(i);
                ref var eventData = ref filter.Get1(i);

                CreateUnit(ref ent, ref eventData);
            }
        }

        /// <summary>
        /// Function for converting unit initialization event into entity
        /// </summary>
        /// <param name="eventData">Event data</param>
        public void CreateUnit(ref EcsEntity ent, ref UnitInitializationEvent eventData)
        {
            // Creating unit game object
            var unitGO = GameObject.Instantiate(eventData.unit.prefab);
            unitGO.transform.position = eventData.position;
            UnitObjectData unitData = unitGO.GetComponent<UnitObjectData>();
            
            unitData.unit = eventData.unit;
            unitData.team = eventData.team;

            InitializeUnit(ref ent, ref unitData);
        }

        /// <summary>
        /// Function for converting unit data (on existing object) into entity
        /// </summary>
        /// <param name="ent">Entity to initialize</param>
        /// <param name="unitData">Unit data component on instantiated object</param>
        public void ConvertGameObject(ref EcsEntity ent, UnitObjectData unitData)
        {
            InitializeUnit(ref ent, ref unitData);
        }

        /// <summary>
        /// Function for converting unit data (on existing object) into entity
        /// </summary>
        /// <param name="ent">Entity to initialize</param>
        /// <param name="unit">Unit settings (ScriptableObject)</param>
        /// <param name="unitData">Unit data component on instantiated object</param>
        public void InitializeUnit(ref EcsEntity ent, ref UnitObjectData unitData)
        {
            // Core
            InitUninitializedTag(ref ent);
            InitUnitComponent(ref ent, ref unitData);
            InitStateComponent(ref ent, unitData.startingState);
            InitHealthComponent(ref ent, ref unitData.unit);

            // Graphics
            InitAnimationComponent(ref ent, ref unitData);
            if (unitData.healthBar)
                InitHealthBarComponent(ref ent, ref unitData);

            // FollowSystem
            if (unitData.unit.moveable)
                InitPathFollowComponent(ref ent, ref unitData.unit);

            // Combat
            InitHealthComponent(ref ent, ref unitData.unit);
            switch (unitData.unit.damageType)
            {
                case DamageType.Melee:
                    InitMeleeDamageComponent(ref ent, ref unitData.unit);
                    break;
                case DamageType.Ranged:
                    InitRangedDamageComponent(ref ent, ref unitData.unit);
                    break;
            }

            // Spawner
            if (unitData.unit.unitSpawner)
            {
                InitUnitSpawnerComponent(ref ent, ref unitData);

                if (unitData.spawnerAiControllable)
                    InitAiSpawnerControlComponent(ref ent, ref unitData.unit);
            }    

            // Removing UnitObjectData component
            GameObject.Destroy(unitData);
        }

        private void InitUninitializedTag(ref EcsEntity ent)
        {
            ent.Get<UninitializedTag>();
        }

        private void InitUnitComponent(ref EcsEntity ent, ref UnitObjectData data)
        {
            ref var unitComponent = ref ent.Get<UnitComponent>();
            unitComponent.transform = data.transform;
            unitComponent.attackRangeAnchor = data.rangeAnchor;
            unitComponent.hitboxAnchor = data.hitboxAnchor;
            
            unitComponent.attackRange = data.unit.attackRange;
            unitComponent.attackSpeed = data.unit.attackSpeed;

            unitComponent.SwapTeam(ref ent, data.team);
            
            // Adding tag
            switch (data.team)
            {
                case Team.Player:
                    ent.Get<PlayerTag>();
                    break;
                case Team.Enemy:
                    ent.Get<EnemyTag>();
                    break;
            }
        }

        private void InitAnimationComponent(ref EcsEntity ent, ref UnitObjectData data)
        {
            ref var animComponent = ref ent.Get<AnimationComponent>();
            animComponent = data.unit.animations;
            animComponent.animator = data.animator;
        }

        private void InitStateComponent(ref EcsEntity ent, UnitState startingState)
        {
            ref var state = ref ent.Get<UnitStateComponent>();
            state.State = startingState;
        }

        private void InitPathFollowComponent(ref EcsEntity ent, ref Unit unit)
        {
            ref var follower = ref ent.Get<PathFollowComponent>();

            follower.path = scene.path.path;
            follower.distance = 0;
            follower.speed = unit.speed;
        }

        private void InitHealthComponent(ref EcsEntity ent, ref Unit unit)
        {
            ref var health = ref ent.Get<HealthComponent>();

            health.maxHealth = unit.maxHP;
            health.Hp = unit.maxHP;
        }

        private void InitHealthBarComponent(ref EcsEntity ent, ref UnitObjectData unitData)
        {
            ref var hbar = ref ent.Get<HealthbarComponent>();

            hbar.healthBar = unitData.healthBar;
        }

        private void InitMeleeDamageComponent(ref EcsEntity ent, ref Unit unit)
        {
            ref var dmg = ref ent.Get<MeleeDamageComponent>();
            dmg.damage = unit.meleeDmgData.damage;
            dmg.fireRate = 1f / unit.attackSpeed;
        }

        private void InitRangedDamageComponent(ref EcsEntity ent, ref Unit unit)
        {
            // TODO
        }

        private void InitUnitSpawnerComponent(ref EcsEntity ent, ref UnitObjectData unitData)
        {
            ref var spawner = ref ent.Get<UnitSpawnerComponent>();
            spawner.spawnPoint = unitData.spawnerPoint;

        }
        private void InitAiSpawnerControlComponent(ref EcsEntity ent, ref Unit unit)
        {
            ref var controller = ref ent.Get<AiSpawnerControlComponent>();
            controller = unit.aiSpawner;
            controller.nextSpawnTime = Time.time + unit.startDealay;
        }
    }
}
