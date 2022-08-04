using Leopotam.Ecs;

namespace TowersBattle.Ecs
{
    public struct UnitStateComponent
    {	
        private UnitState state;

        public UnitState State
        {
            get { return state; }
        }

        public void PushState(EcsEntity ent, UnitState state)
        {
            ref var stateEvent = ref ent.Get<UnitStateChangedEvent>();

            stateEvent.previousState = this.state;
            stateEvent.currentState = state;

            this.state = state;
        }

    }
}
