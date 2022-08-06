using Leopotam.Ecs;
using TowersBattle.Data;

namespace TowersBattle.Ecs
{
    public struct UnitStateComponent
    {
        private UnitState currentState;
        private UnitState lastState;
        private bool changed;

        public UnitState State
        {
            get { return currentState; }
            set {
                if (!changed)
                {
                    lastState = currentState;
                    changed = true;
                }
                currentState = value; 
            }
        }

        public bool IsStateChanged()
        { 
            return changed; 
        }

        public UnitState ReadStateChange(bool resetChange = false)
        {
            changed = !resetChange;
            return lastState;
        }
    }
}
