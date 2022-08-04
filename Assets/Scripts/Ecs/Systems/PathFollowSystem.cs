namespace TowersBattle.Ecs  
{
    using UnityEngine;
    using Leopotam.Ecs;

    /// <summary>
    /// TODO
    /// </summary>
    public class PathFollowSystem : IEcsRunSystem
    {
        private EcsFilter<PathFollowComponent, UnitStateComponent, UnitComponent> filter;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var entity = ref filter.GetEntity(i);

                ref var pathFollower = ref filter.Get1(i);
                ref var unitState = ref filter.Get2(i);
                ref var unit = ref filter.Get3(i);

                // Skipping if unit not in running state
                if (unitState.State != UnitState.Running)
                    continue;

                // Moving unit
                pathFollower.distance += pathFollower.speed * Time.deltaTime;

                // Removing follower component if unit has reaced end
                if (pathFollower.distance >= pathFollower.path.length)
                {
                    unit.transform.position = pathFollower.path.GetPoint(unit.Team == Team.Player ? pathFollower.path.NumPoints - 1 : 0);

                    unitState.PushState(entity, UnitState.Idle);
                    entity.Del<PathFollowComponent>();
                    continue;
                }

                if (unit.Team == Team.Player)
                    unit.transform.position = pathFollower.path.GetPointAtDistance(pathFollower.distance);
                else
                    unit.transform.position = pathFollower.path.GetPointAtDistance(pathFollower.path.length - pathFollower.distance);
            }
        }
    }
}
