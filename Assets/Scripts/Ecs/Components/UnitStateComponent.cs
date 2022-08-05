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

        public void PushState(ref EcsEntity ent, UnitState state)
        {
            ref var stateEvent = ref ent.Get<UnitStateChangedEvent>();
            stateEvent.previousState = this.state;
            stateEvent.currentState = state;

            ref var animationEvent = ref ent.Get<UpdateAnimationEvent>();
            animationEvent.state = state;
            animationEvent.clip = 1;

            this.state = state;
        }

    }
}
