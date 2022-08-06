namespace TowersBattle.Ecs
{
    using Leopotam.Ecs;

    public struct HasTargetComponent
    {
        public EcsEntity target;
        public UnitState previousState;
    }
}
