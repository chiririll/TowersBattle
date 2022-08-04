namespace TowersBattle.Ecs
{
    using UnityEngine;
    using Leopotam.Ecs;

    /// <summary>
    /// TODO
    /// </summary>
    public class AnimationSystem : IEcsRunSystem
    {
        private EcsFilter<UnitStateChangedEvent, UnitComponent> filter;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var stateEvent = ref filter.Get1(i);
                ref var unit = ref filter.Get2(i);

                switch (stateEvent.currentState)
                {
                    case UnitState.Idle:
                        unit.animator.AnimationState.SetAnimation(1, "idle_1", true);
                        break;
                    case UnitState.Running:
                        unit.animator.AnimationState.SetAnimation(1, "run_1", true);
                        break;
                    case UnitState.Attacking:
                        unit.animator.AnimationState.SetAnimation(1, "attack_1", true);
                        break;
                    case UnitState.Dying:
                        unit.animator.AnimationState.SetAnimation(1, "death_1", false);
                        break;
                }
            }
        }
    }
}
