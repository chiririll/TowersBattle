using Leopotam.Ecs;
using TowersBattle.Data;
using UnityEngine;

namespace TowersBattle.Ecs
{
    public class TestInitSystem : IEcsInitSystem
    {
        private EcsWorld ecsWorld;
        private SpawnTable spawnTable;
        
        public void Init()
        {
            //CreateEntity(spawnTable.units[0], Team.Player);
            //CreateEntity(spawnTable.units[1], Team.Player);
            //CreateEntity(spawnTable.units[2], Team.Player);

            //// CreateEntity(spawnTable.units[0], Team.Enemy);
            //CreateEntity(spawnTable.units[1], Team.Enemy);
            //CreateEntity(spawnTable.units[2], Team.Enemy);
        }

        private void CreateEntity(Unit unit, Team team)
        {
            EcsEntity ent = ecsWorld.NewEntity();

            ref var initEvent = ref ent.Get<UnitInitializationEvent>();
            initEvent.unit = unit;
            initEvent.team = team;
            initEvent.position = new Vector3(20 * (team == Team.Player ? -1 : 1), 0, 0);
        }
    }
}
