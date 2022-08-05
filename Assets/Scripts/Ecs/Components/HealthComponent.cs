using System;

namespace TowersBattle.Ecs
{
    public struct HealthComponent
    {
        public int maxHealth;

        private int hp;
        public int Hp
        {
            get { return hp; }
            set {
                if (value >= 0 && value <= maxHealth)
                    hp = value;
                else
                    throw new ArgumentOutOfRangeException("health", value, "out of range (0; " + maxHealth.ToString() + ")");
            }
        }

        public void DealDamage(int damage)
        {
            if (damage < 0)
                throw new ArgumentOutOfRangeException("damage", damage, "below zero");
            
            hp -= damage;
            if (hp < 0)
                hp = 0;
        }

        public void Heal(int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException("amount", amount, "below zero");

            hp += amount;
            if (hp > maxHealth)
                hp = maxHealth;
        }
    }
}
