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

                InitializeUnit(ref ent, unitData);
            }
        }

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var ent = ref filter.GetEntity(i);
                ref var eventData = ref filter.Get1(i);

                InitializeUnit(ref ent, ref eventData);
            }
        }

        /// <summary>
        /// Function for converting unit initialization event into entity
        /// </summary>
        /// <param name="eventData">Event data</param>
        public void InitializeUnit(ref EcsEntity ent, ref UnitInitializationEvent eventData)
        {
            // Creating unit game object
            var unitGO = GameObject.Instantiate(eventData.unit.prefab);
            unitGO.transform.position = eventData.position;
            UnitObjectData unitData = unitGO.GetComponent<UnitObjectData>();

            unitData.team = eventData.team;

            InitializeUnit(ref ent, ref eventData.unit, ref unitData);
        }

        /// <summary>
        /// Function for converting unit data (on existing object) into entity
        /// </summary>
        /// <param name="ent">Entity to initialize</param>
        /// <param name="unitData">Unit data component on instantiated object</param>
        public void InitializeUnit(ref EcsEntity ent, UnitObjectData unitData)
        {
            InitializeUnit(ref ent, ref unitData.unit, ref unitData);
        }

        /// <summary>
        /// Function for converting unit data (on existing object) into entity
        /// </summary>
        /// <param name="ent">Entity to initialize</param>
        /// <param name="unit">Unit settings (ScriptableObject)</param>
        /// <param name="unitData">Unit data component on instantiated object</param>
        public void InitializeUnit(ref EcsEntity ent, ref Unit unit, ref UnitObjectData unitData)
        {
            // Core
            InitUnitComponent(ref ent, ref unitData, ref unit);
            InitStateComponent(ref ent, unitData.startingState);
            InitHealthComponent(ref ent, ref unit);

            // Graphics
            InitAnimationComponent(ref ent, ref unitData, ref unit);
            if (unit.healthBar)
                InitHealthBarComponent(ref ent, ref unitData);

            // FollowSystem
            if (unit.moveable)
                InitPathFollowComponent(ref ent, ref unit);

            // Combat
            InitHealthComponent(ref ent, ref unit);
            switch (unit.damageType)
            {
                case DamageType.Melee:
                    InitMeleeDamageComponent(ref ent, ref unit);
                    break;
                case DamageType.Ranged:
                    InitRangedDamageComponent(ref ent, ref unit);
                    break;
            }

            // Removing UnitObjectData component
            GameObject.Destroy(unitData);
        }

        private void InitUnitComponent(ref EcsEntity ent, ref UnitObjectData data, ref Unit unit)
        {
            ref var unitComponent = ref ent.Get<UnitComponent>();
            unitComponent.transform = data.transform;
            
            unitComponent.attackRange = unit.attackRange;
            unitComponent.attackSpeed = unit.attackSpeed;

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

        private void InitAnimationComponent(ref EcsEntity ent, ref UnitObjectData data, ref Unit unit)
        {
            ref var animComponent = ref ent.Get<AnimationComponent>();
            animComponent = unit.animations;
            animComponent.animator = data.animator;
        }

        private void InitStateComponent(ref EcsEntity ent, UnitState startingState)
        {
            ref var state = ref ent.Get<UnitStateComponent>();
            state.PushState(ref ent, startingState);
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
            
            // TODO
        }

        private void InitMeleeDamageComponent(ref EcsEntity ent, ref Unit unit)
        {
            // TODO
        }

        private void InitRangedDamageComponent(ref EcsEntity ent, ref Unit unit)
        {
            // TODO
        }
    }
}
