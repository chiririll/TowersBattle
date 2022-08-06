using TowersBattle.Data;

namespace TowersBattle.Ecs
{
    public struct UnitStateChangedEvent
    {
        public UnitState previousState;
        public UnitState currentState;
    }
}
