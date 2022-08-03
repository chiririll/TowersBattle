namespace TowersBattle.Data
{
    using Spine.Unity;
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Unit", menuName = "Units/Unit")]
    public class Unit : ScriptableObject
    {
        [Header("Common")]
        public new string name;
        public SkeletonDataAsset skeleton;

        [Header("Preferences")]
        public Unit dominatingTarget;
        [Min(0)] public float dominationFactor;
        // [SerializeField] private Unit prioritizedTarget;

        [Header("Combat")]
        [Min(0)] public int maxHP;
        [Min(0)] public float attackSpeed;
        [Min(0)] public int damage;
        [Min(0)] public float attackRange;

        [Header("Movement")]
        [Min(0)] public float speed;
    }
}
