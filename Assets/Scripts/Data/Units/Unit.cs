namespace TowersBattle.Data
{
    using System;
    using TowersBattle.Ecs;
    using UnityEngine;

    [Serializable]
    public struct Component
    {
        public float test;
    }

    [CreateAssetMenu(fileName = "New Unit", menuName = "Units/Unit")]
    public class Unit : ScriptableObject
    {
        [Header("Common")]
        public new string name;
        public GameObject prefab;

        [Header("Preferences")]
        public Unit dominatingTarget; // TODO!!!
        [Min(0)] public float dominationFactor;

        [Header("Movement")]
        public bool moveable;
        [Min(0)] public float speed;

        [Header("Combat")]
        [Min(0)] public int maxHP;
        [Min(0)] public float attackSpeed;
        [Min(0)] public float attackRange;

        [Space(5)]
        public DamageType damageType;
        public MeleeDamageData meleeDmgData;
        public RangedDamageData rangedDmgData;

        [Header("Spawner")]
        public bool unitSpawner;
        public float startDealay;
        public AiSpawnerControlComponent aiSpawner;

        [Header("Graphics")]
        public AnimationComponent animations;
    }
}
