namespace TowersBattle.Data
{
    using Spine.Unity;
    using TowersBattle.Ecs;
    using UnityEngine;

    public class UnitObjectData : MonoBehaviour 
    {
        // DefaultProperties
        public Unit unit;
        public Team team;
        public UnitState startingState = UnitState.Running;

        // Animation
        public SkeletonAnimation animator;
    }
}
