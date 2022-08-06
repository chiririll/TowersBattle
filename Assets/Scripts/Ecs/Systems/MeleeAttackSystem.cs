namespace TowersBattle.Ecs
{
    using UnityEngine;
    using Leopotam.Ecs;

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
                ref var dmgComponent = ref filter.Get2(i);

                // Skipping entity if cooldown
                if (dmgComponent.nextAttack > Time.time)
                    continue;

                ref var unitEnt = ref filter.GetEntity(i);
                ref var targetComponent = ref filter.Get1(i);
                ref var unit = ref filter.Get3(i);
                ref var unitState = ref filter.Get4(i);

                // Updating animation
                CallAnimationEvent(ref unitEnt);

                ref var targetEnt = ref targetComponent.target;
                ref var target = ref targetEnt.Get<UnitComponent>();
                ref var targetState = ref targetEnt.Get<UnitStateComponent>();
                ref var targetHealth = ref targetEnt.Get<HealthComponent>();

                // Attacking
                if (targetState.State == UnitState.Dying || targetState.State == UnitState.Destroying)
                {
                    unitState.State = targetComponent.previousState;
                    unitEnt.Del<HasTargetComponent>();
                    continue;
                }

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
