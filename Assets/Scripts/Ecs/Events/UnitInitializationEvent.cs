namespace TowersBattle.Ecs
{
    using TowersBattle.Data;
    using UnityEngine;

    public struct UnitInitializationEvent 
    {
        public Unit unit;
        public Team team;

        public Vector3 position;
    }
}
