using UnityEngine;
using Leopotam.Ecs;

namespace TowersBattle.Ecs
{
    /// <summary>
    /// TODO
    /// </summary>
    public class MeleeAttackSystem : IEcsRunSystem
    {
        private EcsFilter<HasTargetComponent, MeleeDamageComponent, UnitComponent, UnitStateComponent>.Exclude<DeadTag> filter;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var targetComponent = ref filter.Get1(i);
                ref var dmgComponent = ref filter.Get2(i);
                ref var unit = ref filter.Get3(i);
                ref var unitState = ref filter.Get4(i);

                ref var unitEnt = ref filter.GetEntity(i);
                ref var targetEnt = ref targetComponent.target;
                
                ref var target = ref targetEnt.Get<UnitComponent>();
                ref var targetState = ref targetEnt.Get<UnitStateComponent>();

                // Skipping entity if cooldown
                if (dmgComponent.nextAttack > Time.time)
                    continue;

                // If target dead
                if (targetState.State == UnitState.Dying || targetState.State == UnitState.Destroying)
                {
                    unitState.State = targetComponent.previousState;
                    unitEnt.Del<HasTargetComponent>();
                    continue;
                }

                // Updating animation
                CallAnimationEvent(ref unitEnt);

                
                ref var targetHealth = ref targetEnt.Get<HealthComponent>();

                // Attacking
                targetHealth.DealDamage(dmgComponent.damage);
                dmgComponent.nextAttack = Time.time + dmgComponent.fireRate;
            }    
        }

        private void CallAnimationEvent(ref EcsEntity ent)
        {
            ref var animEvent = ref ent.Get<UpdateAnimationEvent>();
            animEvent.state = UnitState.Attacking;
            animEvent.clip = 1;
        }
    }
}
