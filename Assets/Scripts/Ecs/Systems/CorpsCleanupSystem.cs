using Leopotam.Ecs;
using UnityEngine;

namespace TowersBattle.Ecs
{
    /// <summary>
    /// TODO
    /// </summary>
    public class CorpsCleanupSystem : IEcsRunSystem
    {
        public const float CleanupTime = 2f;

        private EcsFilter<DeadTag, UnitComponent>.Exclude<CleanupTimerComponent> attachTimerFilter;
        private EcsFilter<CleanupTimerComponent, UnitComponent> destroyFilter;

        public void Run()
        {
            foreach (var i in attachTimerFilter)
            {
                ref var timer = ref attachTimerFilter.GetEntity(i).Get<CleanupTimerComponent>();
                timer.destroyTime = Time.time + CleanupTime;
            }

            foreach (var i in destroyFilter)
            {
                ref var timer = ref destroyFilter.Get1(i);

                if (Time.time < timer.destroyTime)
                    continue;

                ref var unit = ref destroyFilter.Get2(i);
                GameObject.Destroy(unit.transform.gameObject);
                destroyFilter.GetEntity(i).Destroy();
            }
        }
    }
}
