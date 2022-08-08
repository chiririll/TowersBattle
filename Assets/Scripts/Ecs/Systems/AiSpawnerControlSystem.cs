using Leopotam.Ecs;
using TowersBattle.Data;
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

                if (Time.time < controller.nextActionTime)
                    continue;

                if (controller.currentWave >= controller.waves.Length)
                {
                    // Waves is over, removing spawner
                    ref var ent = ref filter.GetEntity(i);
                    
                    ent.Del<AiSpawnerControlComponent>();
                    ent.Del<UnitSpawnerComponent>();
                    continue;
                }

                // Getting unit to spawn
                Unit unit = controller.waves[controller.currentWave].GetUnit();
                if (unit == null)
                {
                    // TODO: Send wave event

                    // Next wave
                    controller.nextActionTime = Time.time + controller.interval.Get();
                    controller.currentWave++;
                    continue;
                }
                
                // Spawning unit
                ref var spawnEvent = ref filter.GetEntity(i).Get<SpawnUnitEvent>();
                spawnEvent.unit = unit;

                // Adding cooldown
                controller.nextActionTime = Time.time + controller.waves[controller.currentWave].cooldown.Get();
            }
        }
    }
}
