using Leopotam.Ecs;
using TowersBattle.Data;

namespace TowersBattle.Ecs
{
    public struct HasTargetComponent
    {
        public UnitState unitPreviousState;

        public UnitType type;
        public EcsEntity entity;

        public UnitState State {
            get { return entity.IsAlive() ? entity.Get<UnitStateComponent>().State : UnitState.Destroying; }
        }
    }
}
