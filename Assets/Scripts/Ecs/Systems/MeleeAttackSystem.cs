using Leopotam.Ecs;

namespace TowersBattle.Ecs
{
    /// <summary>
    /// TODO
    /// </summary>
    public class MeleeAttackSystem : IEcsRunSystem
    {
        private EcsFilter<AttackEvent, MeleeDamageComponent, HasTargetComponent> filter;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var attackEvent = ref filter.Get1(i);
                ref var melee = ref filter.Get2(i);
                ref var target = ref filter.Get3(i);

                target.entity.Get<HealthComponent>().DealDamage((int)(melee.damage * attackEvent.damageFactor));
            }
        }
    }
}
