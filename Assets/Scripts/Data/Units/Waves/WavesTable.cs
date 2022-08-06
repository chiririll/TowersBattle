using UnityEngine;

namespace TowersBattle.Data
{
    /// <summary>
    /// TODO
    /// </summary>
    [CreateAssetMenu(fileName = "New Waves table", menuName = "Units/Waves table")]
    public class WavesTable : ScriptableObject 
    {
        public float minWaveDelay;
        public float maxWaveDelay;

        public UnitWave[] waves;

        private int currentWave;
        private float waveStart;

        public Unit GetUnit()
        {
            if (Time.time < waveStart || currentWave >= waves.Length)
                return null;

            return null;
        }
    }
}
