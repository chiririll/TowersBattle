using Leopotam.Ecs;
using TowersBattle.Data;
using UnityEngine;

namespace TowersBattle.Ecs  
{
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

                // Getting nearest point 
                if (pathFollower.distance == 0)
                {
                    float dist = pathFollower.path.GetClosestDistanceAlongPath(unit.transform.position);
                    pathFollower.distance = unit.Team == Team.Player ? dist : pathFollower.path.length - dist;
                }

                // Skipping if unit not in running state
                if (unitState.State != UnitState.Running)
                    continue;

                // Moving unit
                pathFollower.distance += pathFollower.speed * Time.deltaTime;

                // Removing follower component if unit has reaced end
                if (pathFollower.distance >= pathFollower.path.length)
                {
                    unit.transform.position = pathFollower.path.GetPoint(unit.Team == Team.Player ? pathFollower.path.NumPoints - 1 : 0);

                    unitState.State = UnitState.Idle;
                    entity.Del<PathFollowComponent>();
                    continue;
                }

                if (unit.Team == Team.Player)
                    unit.transform.position = pathFollower.path.GetPointAtDistance(pathFollower.distance);
                else
                    unit.transform.position = pathFollower.path.GetPointAtDistance(pathFollower.path.length - pathFollower.distance);
            }
        }

        //void changeClip()
        //{
        //    // Changing animation
        //    float angle = pathFollower.path.GetRotationAtDistance(pathFollower.distance).eulerAngles.x;

        //    int clip;
        //    if (angle > 45 && angle < 135 || angle > 225 && angle < 315)
        //        clip = (unit.Team == Team.Player ? 1 : 2);
        //    else
        //        clip = (unit.Team == Team.Player ? 2 : 1);

        //    if (clip != pathFollower.lastClip)
        //    {
        //        ref var animEvent = ref entity.Get<UpdateAnimationEvent>();
        //        animEvent.state = UnitState.Running;
        //        animEvent.clip = clip;
        //    }
        //    pathFollower.lastClip = clip;
        //}
    }
}
