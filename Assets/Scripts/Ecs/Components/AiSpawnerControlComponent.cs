using System;
using TowersBattle.Data;
using UnityEngine;

namespace TowersBattle.Ecs
{
    [Serializable]
    public struct AiSpawnerControlComponent
    {
        // TODO: Refactor
        public SpawnTable table;

        [Min(0)] public float minCooldown;
        [Min(0)] public float maxCooldown;
        
        [HideInInspector] public float nextSpawnTime;
    }
}
