using UnityEngine;

namespace TowersBattle.Data
{
    /// <summary>
    /// TODO
    /// </summary>
    [System.Serializable]
    public class Cooldown
    {
        [Min(0)][SerializeField] private float cooldown;
        
        [SerializeField] private bool useRandomCooldown;
        [Min(0)][SerializeField] private float minCooldown;
        [Min(0)][SerializeField] private float maxCooldown;

        public float Get()
        {
            if (useRandomCooldown)
                return Random.Range(minCooldown, maxCooldown);
            return cooldown;
        }
    }
}
