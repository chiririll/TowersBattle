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
        public UnitType type;
        public GameObject prefab;
        public Sprite icon;
        
        [Header("Movement")]
        public bool moveable;
        [Min(0)] public float speed;

        [Header("Combat")]
        [Min(0)] public int maxHP;
        public AttackComponent attackData;

        [Space(5)]
        public DamageType damageType;
        public MeleeDamageComponent meleeDmgData;
        public RangedDamageComponent rangedDmgData;

        [Header("Spawner")]
        public bool unitSpawner;
        public float startDealay;
        public AiSpawnerControlComponent aiSpawner;

        [Header("Graphics")]
        public bool cleanupCorpse = true;
        public AnimationComponent animations;
    }
}
