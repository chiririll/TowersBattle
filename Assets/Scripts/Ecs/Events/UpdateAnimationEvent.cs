using TowersBattle.Data;

namespace TowersBattle.Ecs
{
    public struct UpdateAnimationEvent
    {
        public UnitState state;
        public int clip;
    }
}
