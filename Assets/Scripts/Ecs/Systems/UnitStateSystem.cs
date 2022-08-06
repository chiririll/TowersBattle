using Leopotam.Ecs;

namespace TowersBattle.Ecs
{
    /// <summary>
    /// TODO
    /// </summary>
    public class UnitStateSystem : IEcsRunSystem
    {
        private EcsFilter<UnitStateComponent> filter;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var stateComp = ref filter.Get1(i);
                if (stateComp.IsStateChanged())
                    CallStateChangedEvent(ref filter.GetEntity(i), ref stateComp);
            }
        }

        private void CallStateChangedEvent(ref EcsEntity ent, ref UnitStateComponent stateComp)
        {
            ref var stateEvent = ref ent.Get<UnitStateChangedEvent>();
            stateEvent.previousState = stateComp.ReadStateChange(true);
            stateEvent.currentState = stateComp.State;
        }
    }
}
