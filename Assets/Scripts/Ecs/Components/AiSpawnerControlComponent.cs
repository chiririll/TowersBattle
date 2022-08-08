using TowersBattle.Data;
using TowersBattle.Data.Waves;

namespace TowersBattle.Ecs
{
    public struct AiSpawnerControlComponent
    {
        public Wave[] waves;
        public Cooldown interval;

        public int currentWave;
        public float nextActionTime;
    }
}
