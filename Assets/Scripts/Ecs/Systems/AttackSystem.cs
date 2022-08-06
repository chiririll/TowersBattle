using Leopotam.Ecs;
using TowersBattle.Data;
using UnityEngine;

namespace TowersBattle.Ecs
{
    /// <summary>
    /// TODO
    /// </summary>
    public class AttackSystem : IEcsRunSystem
    {
        private EcsFilter<HasTargetComponent, AttackComponent, UnitStateComponent>.Exclude<DeadTag> filter;

        public void Run()
        {
            // Get components
            // Check cooldown
            // Check entity
            // Call event

            foreach (var i in filter)
            {
                // Getting components
                ref var target = ref filter.Get1(i);
                ref var attackComponent = ref filter.Get2(i);
                ref var unitState = ref filter.Get3(i);
                // And entity
                ref var unitEntity = ref filter.GetEntity(i);

                // If animation cooldown
                if (!attackComponent.CanCheck())
                    continue;

                // Checking target state
                if (target.State == UnitState.Dying || target.State == UnitState.Destroying)
                {
                    FinishFight(ref unitEntity, ref unitState, target.unitPreviousState);
                    continue;
                }

                // Skipping entity if cooldown
                if (!attackComponent.CanAttack())
                    continue;

                attackComponent.Attack();

                // Creating attack event
                ref var attackEvent = ref unitEntity.Get<AttackEvent>();
                if (attackComponent.dominatingTarget == target.type)
                    attackEvent.damageFactor = attackComponent.dominationFactor;
                else
                    attackEvent.damageFactor = 1f;

            }    
        }

        private void FinishFight(ref EcsEntity unitEnt, ref UnitStateComponent unitState, UnitState lastState)
        {
            unitState.State = lastState;
            unitEnt.Del<HasTargetComponent>();
        }
    }
}
