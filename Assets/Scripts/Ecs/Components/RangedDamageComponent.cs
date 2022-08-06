using System;

namespace TowersBattle.Ecs
{
    [Serializable]
    public struct RangedDamageComponent
    {
        // public Projectile projectile;
        public float fireRate;
        public int damage;
    }
}
