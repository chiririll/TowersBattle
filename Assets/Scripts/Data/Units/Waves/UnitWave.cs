using System;
using UnityEngine;

namespace TowersBattle.Data
{
    [Serializable]
    public class UnitWave
    {
        [Min(0)] public float minSpawnDelay;
        [Min(0)] public float maxSpawnDelay;

        private float spawnDelay;

        public UnitQuantity[] units;
    }
}
