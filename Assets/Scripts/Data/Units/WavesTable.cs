using TowersBattle.Data.Waves;
using UnityEngine;

namespace TowersBattle.Data
{
    /// <summary>
    /// TODO
    /// </summary>
    [CreateAssetMenu(fileName = "New Waves table", menuName = "Units/Waves table")]
    public class WavesTable : ScriptableObject 
    {
        public Cooldown interval;

        public Wave[] waves;
    }
}
