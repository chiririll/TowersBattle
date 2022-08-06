using UnityEngine;
using Leopotam.Ecs;

namespace TowersBattle.Ecs
{
    /// <summary>
    /// TODO
    /// </summary>
    public class HealthSystem : IEcsRunSystem
    {
        private EcsFilter<HealthComponent, UnitStateComponent>.Exclude<DeadTag> filter;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var hp = ref filter.Get1(i);

                if (hp.IsHealthChanged())
                    CallHpChangedEvent(ref filter.GetEntity(i), ref hp);

                if (hp.Hp == 0)
                    MarkDead(ref filter.GetEntity(i), ref filter.Get2(i));
            }    
        }

        private void CallHpChangedEvent(ref EcsEntity ent, ref HealthComponent hp)
        {
            ref var hpEvent = ref ent.Get<HealthChangedEvent>();

            hpEvent.hp = hp.Hp;
            hpEvent.delta = hp.ReadHpChanges(true);
        }

        private void MarkDead(ref EcsEntity ent, ref UnitStateComponent unitState)
        {
            unitState.State = UnitState.Dying;

            ref var deathEvent = ref ent.Get<UnitDeathEvent>();
            
            ent.Get<DeadTag>();
        }
    }
}
