using UnityEngine;

namespace TowersBattle.Data
{
    /// <summary>
    /// TODO
    /// </summary>
    [CreateAssetMenu(fileName = "New Level", menuName = "Units/Level settings")]
    public class LevelSettings : ScriptableObject
    {
        [Header("Player settings")]
        public Cooldown playerCooldown;
        public SpawnTable playerUnits;

        [Header("Enemy settings")]
        [Min(0)] public float startDelay;
        public WavesTable enemyWaves;

        [Header("Display Setting")]
        public string levelName;
        public string sceneName;
        public Sprite levelIcon;
    }
}
