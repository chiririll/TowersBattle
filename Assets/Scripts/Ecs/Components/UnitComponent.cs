namespace TowersBattle.Ecs
{
    using Spine.Unity;
    using UnityEngine;

    public struct UnitComponent
    {
        // Unity components
        public Transform transform;
        public Collider2D rangeCollider;
        public Collider2D hitbox;
        public SkeletonAnimation animator;

        // Combat
        private Team team;
        public Team Team
        {
            get { return team; }
            set {
                animator.skeleton.SetSkin(value == Team.Player ? "1" : "2");
                animator.skeleton.ScaleX = Mathf.Abs(animator.skeleton.ScaleX) * (value == Team.Player ? 1 : -1);
                team = value; 
            }
        }
        public float fireRate;
    }
}
