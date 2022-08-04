namespace TowersBattle.Ecs
{
    using Leopotam.Ecs;
    using Spine.Unity;
    using TowersBattle.Data;
    using UnityEngine;

    public class TestInitSystem : IEcsInitSystem
    {
        private EcsWorld ecsWorld;
        
        private SpawnTable spawnTable;
        private SceneContext scene;

        public void Init()
        {
            CreateEntity(spawnTable.units[0], Team.Player);
            CreateEntity(spawnTable.units[1], Team.Player);
            CreateEntity(spawnTable.units[2], Team.Player);

            CreateEntity(spawnTable.units[0], Team.Enemy);
            CreateEntity(spawnTable.units[1], Team.Enemy);
            CreateEntity(spawnTable.units[2], Team.Enemy);
        }

        private void CreateEntity(Unit unitProperties, Team team)
        {
            EcsEntity unit = ecsWorld.NewEntity();

            ref var unitData = ref unit.Get<UnitComponent>();
            ref var state = ref unit.Get<UnitStateComponent>();
            ref var follower = ref unit.Get<PathFollowComponent>();

            var unitGO = GameObject.Instantiate(unitProperties.prefab);

            unitData.transform = unitGO.transform;
            unitData.animator = unitGO.GetComponent<SkeletonAnimation>();
            unitData.fireRate = unitProperties.attackSpeed;
            unitData.Team = team;

            state.PushState(unit, UnitState.Running);

            follower.path = scene.path.path;
            follower.distance = 0; // default
            follower.speed = unitProperties.speed;
        }
    }
}
