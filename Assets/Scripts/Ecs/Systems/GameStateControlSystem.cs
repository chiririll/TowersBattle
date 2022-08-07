using Leopotam.Ecs;
using TowersBattle.Core;
using TowersBattle.Data;
using UnityEngine;

namespace TowersBattle.Ecs
{
    /// <summary>
    /// TODO
    /// </summary>
    public class GameStateControlSystem : IEcsRunSystem
    {
        private GameManager gameManager;

        private EcsFilter<PlayerMainTag>.Exclude<DeadTag> palyerUnits;
        private EcsFilter<EnemyMainTag>.Exclude<DeadTag> enemyUnits;

        private EcsFilter<UnitStateComponent>.Exclude<CleanupTimerComponent, DontCleanTag> aliveEntities;

        public void Run()
        {
            if (enemyUnits.GetEntitiesCount() == 0)
                FinishGame(GameState.Victory);
            else if (palyerUnits.GetEntitiesCount() == 0)
                FinishGame(GameState.GameOver);
        }

        private void FinishGame(GameState gameResult)
        {
            gameManager.PushState(gameResult);

            foreach (var i in aliveEntities)
            {
                ref var unitState = ref aliveEntities.Get1(i);
                ref var timer = ref aliveEntities.GetEntity(i).Get<CleanupTimerComponent>();

                if (unitState.State != UnitState.Dying && unitState.State != UnitState.Destroying)
                    unitState.State = UnitState.Idle;

                // FIXME: hardcode below
                timer.destroyTime = Time.time + Random.Range(2f, 4f);
            }
        }
    }
}
