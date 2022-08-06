using System;
using TowersBattle.Data;
using UnityEngine;

namespace TowersBattle.Ecs
{
    [Serializable]
    public struct AttackComponent
    {
        [Min(0)] public float attackRange;
        [Min(0)] public float attackSpeed;

        public UnitType dominatingTarget;
        [Min(0)] public float dominationFactor;

        [HideInInspector] public float nextAttack;
        [HideInInspector] public float nextCheck;

        public void Attack()
        {
            if (!CanAttack())
                return;

            nextAttack = Time.time + 1f / attackSpeed;
            nextCheck = Time.time + 1f / attackSpeed / 2f;
        }

        public bool CanAttack()
        {
            return Time.time >= nextAttack;
        }

        public bool CanCheck()
        {
            return Time.time >= nextCheck;
        }
    }
}
