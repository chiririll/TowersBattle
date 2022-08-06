using Leopotam.Ecs;
using UnityEngine;

namespace TowersBattle.Ecs
{
    /// <summary>
    /// TODO
    /// </summary>
    public class AiSpawnerControlSystem : IEcsRunSystem
    {
        private EcsFilter<AiSpawnerControlComponent>.Exclude<DeadTag> filter;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var controller = ref filter.Get1(i);

                if (Time.time < controller.nextSpawnTime)
                    continue;

                controller.nextSpawnTime = Time.time + Random.Range(controller.minCooldown, controller.maxCooldown);

                ref var spawnEvent = ref filter.GetEntity(i).Get<SpawnUnitEvent>();
                spawnEvent.unit = controller.table.GetUnit();
            }
        }
    }
}
