using Leopotam.Ecs;

namespace TowersBattle.Ecs
{
    /// <summary>
    /// TODO
    /// </summary>
    public class PlayerSpawnerControlSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerUnitSpawnRequest> requestsFilter;
        private EcsFilter<PlayerControllableComponent> controllablesFilter;

        public void Run()
        {
            foreach (var i in requestsFilter)
            {
                ref var request = ref requestsFilter.Get1(i);

                foreach (var j in controllablesFilter)
                {
                    // ref var inputComponent = ref controllablesFilter.Get1(j);

                    ref var spawnEvent = ref controllablesFilter.GetEntity(j).Get<SpawnUnitEvent>();
                    spawnEvent.unit = request.unit;
                }

                requestsFilter.GetEntity(i).Destroy();
            }
        }
    }
}
