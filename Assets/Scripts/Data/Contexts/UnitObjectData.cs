namespace TowersBattle.Data
{
    using Spine.Unity;
    using TowersBattle.Ecs;
    using TowersBattle.UI;
    using UnityEngine;
    using UnityEditor;

    public class UnitObjectData : MonoBehaviour 
    {
        [Header("Fields for existing entity")]
        public Team team;
        public Unit unit;

        [Header("Initialization")]
        public UnitState startingState = UnitState.Running;
        public Vector3 rangeAnchor;
        public Vector3 hitboxAnchor;

        [Header("Component references")]
        public SkeletonAnimation animator;
        public BaseHealthBar healthBar;

        #if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (unit == null)
                return;

            Color blue = new Color(0f, 0f, 2f);
            Color red = new Color(2f, 0f, 0f);

            // Drawing range anchor
            Gizmos.color = blue;
            Gizmos.DrawSphere(transform.position + rangeAnchor, .1f);

            // Drawing hitbox anchor
            Gizmos.color = red;
            Gizmos.DrawSphere(transform.position + hitboxAnchor, .1f);

            // Drawing attack range
            Handles.color = blue;
            Handles.DrawWireDisc(transform.position + rangeAnchor, transform.forward, unit.attackRange);
            
        }
        #endif
    }
}
