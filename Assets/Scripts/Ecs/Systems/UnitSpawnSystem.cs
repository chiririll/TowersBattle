using UnityEngine;
using Leopotam.Ecs;
using TowersBattle.Data;

namespace TowersBattle.Ecs
{
    /// <summary>
    /// TODO
    /// </summary>
    public class UnitSpawnSystem : IEcsRunSystem 
    {
        private EcsWorld ecsWorld;
        private EcsFilter<SpawnUnitEvent, UnitSpawnerComponent, UnitComponent>.Exclude<DeadTag> filter;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var spawnEvent = ref filter.Get1(i);
                ref var spawner = ref filter.Get2(i);
                ref var unit = ref filter.Get3(i);
                
                SpawnEntity(spawnEvent.unit, unit.Team, unit.transform.position + spawner.spawnPoint);
            }
        }

        private void SpawnEntity(Unit unit, Team team, Vector3 position)
        {
            EcsEntity ent = ecsWorld.NewEntity();

            ref var initEvent = ref ent.Get<UnitInitializationEvent>();
            initEvent.unit = unit;
            initEvent.team = team;
            initEvent.position = position;
        }
    }
}
