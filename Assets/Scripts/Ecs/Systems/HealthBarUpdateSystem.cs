using Leopotam.Ecs;
using UnityEngine;

namespace TowersBattle.Ecs
{
    /// <summary>
    /// TODO
    /// </summary>
    public class HealthBarUpdateSystem : IEcsRunSystem
    {
        private EcsFilter<UninitializedTag, HealthbarComponent, HealthComponent, UnitComponent> initializeFilter;
        private EcsFilter<HealthChangedEvent, HealthbarComponent> updateHealthFilter;
        private EcsFilter<DeadTag, HealthbarComponent> deadFilter;

        public void Run()
        {
            // Initializing 
            foreach (var i in initializeFilter)
            {
                ref var hbar = ref initializeFilter.Get2(i);
                ref var health = ref initializeFilter.Get3(i);
                ref var unit = ref initializeFilter.Get4(i);

                hbar.healthBar.InitMaxHealth(health.maxHealth);
                hbar.healthBar.InitTeam(unit.Team);
            }

            // Updating
            foreach (var i in updateHealthFilter)
            {
                ref var healthEvent = ref updateHealthFilter.Get1(i);
                ref var hbar = ref updateHealthFilter.Get2(i);

                hbar.healthBar.UpdateHealth(healthEvent.hp);
            }

            // Destroying
            foreach (var i in deadFilter)
            {
                ref var hbar = ref deadFilter.Get2(i);
                hbar.healthBar.DestroyHbar();
                deadFilter.GetEntity(i).Del<HealthbarComponent>();
            }
        }
    }
}
