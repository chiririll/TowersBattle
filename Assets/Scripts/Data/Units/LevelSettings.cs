using UnityEngine;

namespace TowersBattle.Data
{
    /// <summary>
    /// TODO
    /// </summary>
    [CreateAssetMenu(fileName = "New Level", menuName = "Levels/Level settings")]
    public class LevelSettings : ScriptableObject
    {
        [Header("Player settings")]
        [Min(0)][SerializeField] private float cooldown;
        [SerializeField] private bool randomCooldown;
        [Min(0)][SerializeField] public float cooldownMin;
        [Min(0)][SerializeField] public float cooldownMax;
        public SpawnTable playerUnits;

        [Header("Enemy settings")]
        public WavesTable enemyWaves;

        public float GetCooldown()
        {
            if (!randomCooldown)
                return cooldown;
            return Random.Range(cooldownMin, cooldownMax);
        }
    }
}
