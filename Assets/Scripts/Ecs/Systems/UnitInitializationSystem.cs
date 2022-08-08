using Leopotam.Ecs;
using System;
using TowersBattle.Core;
using TowersBattle.Data;
using TowersBattle.Data.Waves;
using UnityEngine;

namespace TowersBattle.Ecs
{
    /// <summary>
    /// TODO
    /// </summary>
    public class UnitInitializationSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld ecsWorld;
        private SceneEntityConvertor scene;
        private GameManager gameManager;

        private EcsFilter<UnitInitializationEvent> filter;

        public void Init()
        {
            foreach (UnitObjectData unitData in scene.units)
            {
                EcsEntity ent = ecsWorld.NewEntity();

                ConvertGameObject(ref ent, unitData, true);
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
        public void ConvertGameObject(ref EcsEntity ent, UnitObjectData unitData, bool fromScene = false)
        {
            InitializeUnit(ref ent, ref unitData, fromScene);
        }

        /// <summary>
        /// Function for converting unit data (on existing object) into entity
        /// </summary>
        /// <param name="ent">Entity to initialize</param>
        /// <param name="unit">Unit settings (ScriptableObject)</param>
        /// <param name="unitData">Unit data component on instantiated object</param>
        public void InitializeUnit(ref EcsEntity ent, ref UnitObjectData unitData, bool fromScene = false)
        {
            // Main tags
            if (fromScene)
                InitMainTag(ref ent, unitData.team);

            // Core
            InitUninitializedTag(ref ent);
            InitUnitComponent(ref ent, ref unitData, fromScene);
            InitStateComponent(ref ent, unitData.startingState);
            InitHealthComponent(ref ent, ref unitData.unit);
            InitAttackComponent(ref ent, ref unitData.unit);

            // Graphics
            InitAnimationComponent(ref ent, ref unitData);
            if (unitData.healthBar)
                InitHealthBarComponent(ref ent, ref unitData);
            if (!unitData.unit.cleanupCorpse)
                InitDontCleanTag(ref ent);

            // Sounds
            InitSoundComponent(ref ent, ref unitData);

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
                InitUnitSpawner(ref ent, ref unitData);

            // Removing UnitObjectData component
            GameObject.Destroy(unitData);
        }

        private void InitMainTag(ref EcsEntity ent, Team team)
        {
            switch (team)
            {
                case Team.Player:
                    ent.Get<PlayerMainTag>();
                    break;
                case Team.Enemy:
                    ent.Get<EnemyMainTag>();
                    break;
            }
        }

        private void InitUninitializedTag(ref EcsEntity ent)
        {
            ent.Get<UninitializedTag>();
        }

        private void InitUnitComponent(ref EcsEntity ent, ref UnitObjectData data, bool fromScene = false)
        {
            ref var unitComponent = ref ent.Get<UnitComponent>();
            unitComponent.type = data.unit.type;

            unitComponent.transform = data.transform;
            unitComponent.attackRangeAnchor = data.rangeAnchor;
            unitComponent.hitboxAnchor = data.hitboxAnchor;

            unitComponent.SwapTeam(ref ent, data.team, !fromScene);
            
            // Adding tag
            switch (data.team)
            {
                case Team.Player:
                    ent.Get<PlayerMainTag>();
                    break;
                case Team.Enemy:
                    ent.Get<EnemyMainTag>();
                    break;
            }
        }

        private void InitAnimationComponent(ref EcsEntity ent, ref UnitObjectData data)
        {
            ref var animComponent = ref ent.Get<AnimationComponent>();
            animComponent = data.unit.animations;
            animComponent.animator = data.animator;
        }

        private void InitSoundComponent(ref EcsEntity ent, ref UnitObjectData data)
        {
            if (data.soundSource == null)
                return;

            ref var soundComponent = ref ent.Get<SoundComponent>();
            soundComponent = data.unit.sounds;
            soundComponent.soundSource = data.soundSource;
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

        private void InitDontCleanTag(ref EcsEntity ent)
        {
            ent.Get<DontCleanTag>();
        }

        private void InitHealthComponent(ref EcsEntity ent, ref Unit unit)
        {
            ref var health = ref ent.Get<HealthComponent>();

            health.maxHealth = unit.maxHP;
            health.Hp = unit.maxHP;
        }

        private void InitAttackComponent(ref EcsEntity ent, ref Unit unit)
        {
            ref var attackComp = ref ent.Get<AttackComponent>();

            attackComp = unit.attackData;
        }

        private void InitHealthBarComponent(ref EcsEntity ent, ref UnitObjectData unitData)
        {
            ref var hbar = ref ent.Get<HealthbarComponent>();

            hbar.healthBar = unitData.healthBar;
        }

        private void InitMeleeDamageComponent(ref EcsEntity ent, ref Unit unit)
        {
            ref var dmg = ref ent.Get<MeleeDamageComponent>();
            dmg = unit.meleeDmgData;
        }

        private void InitRangedDamageComponent(ref EcsEntity ent, ref Unit unit)
        {
            // TODO
        }

        private void InitUnitSpawner(ref EcsEntity ent, ref UnitObjectData unitData)
        {
            ref var spawner = ref ent.Get<UnitSpawnerComponent>();
            spawner.spawnPoint = unitData.spawnerPoint;

            switch (unitData.spawnerControlType)
            {
                case ControlType.Player:
                    InitPlayerSpawnerControlComponent(ref ent);
                    break;
                case ControlType.AI:
                    InitAiSpawnerControlComponent(ref ent);
                    break;
            }
        }

        private void InitAiSpawnerControlComponent(ref EcsEntity ent)
        {
            ref var controller = ref ent.Get<AiSpawnerControlComponent>();

            ref var table = ref gameManager.LevelSettings.enemyWaves;

            // Copying waves
            controller.waves = new Wave[table.waves.Length];
            for (int i = 0; i < table.waves.Length; i++)
                controller.waves[i] = new Wave(table.waves[i]);

            controller.interval = table.interval;

            // Adding start cooldown
            controller.nextActionTime = Time.time + gameManager.LevelSettings.startDelay;
        }

        private void InitPlayerSpawnerControlComponent(ref EcsEntity ent)
        {
            ref var controller = ref ent.Get<PlayerControllableComponent>();
        }
    }
}
