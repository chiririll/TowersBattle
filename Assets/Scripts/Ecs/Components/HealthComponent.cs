using System;
using UnityEngine;

namespace TowersBattle.Ecs
{
    public struct HealthComponent
    {
        public int maxHealth;

        private int currentHp;
        private bool changed;
        private int lastHp;

        public int Hp
        {
            get { return currentHp; }
            set {
                if (!changed)
                {
                    lastHp = currentHp;
                    changed = true;
                }
                currentHp = Mathf.Clamp(value, 0, maxHealth);
            }
        }

        public void DealDamage(int damage)
        {
            if (damage < 0)
                throw new ArgumentOutOfRangeException("damage", damage, "below zero");
            
            Hp -= damage;
        }

        public void Heal(int healAmount)
        {
            if (healAmount < 0)
                throw new ArgumentOutOfRangeException("healAmount", healAmount, "below zero");

            Hp += healAmount;
        }

        public bool IsHealthChanged()
        {
            return changed;
        }

        public int ReadHpChanges(bool resetChanges = false)
        {
            changed = !resetChanges;
            return lastHp;
        }
    }
}
