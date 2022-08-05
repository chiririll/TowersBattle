namespace TowersBattle.Ecs
{
    using Leopotam.Ecs;
    using UnityEngine;

    /// <summary>
    /// TODO
    /// </summary>
    public class TargetFindingSystem : IEcsRunSystem
    {
        private EcsFilter<UnitComponent, UnitStateComponent>.Exclude<HasTargetComponent, DeadTag> filterNeedTarget;
        private EcsFilter<UnitComponent, HealthComponent>.Exclude<DeadTag> filterAllUnits;

        public void Run()
        {
            foreach (var i in filterNeedTarget)
            {
                ref var ent = ref filterNeedTarget.GetEntity(i);
                ref var unit = ref filterNeedTarget.Get1(i);
                ref var unitState = ref filterNeedTarget.Get2(i);
                    
                FindTarget(ref ent, ref unit, ref unitState);
            }
        }

        private void FindTarget(ref EcsEntity ent, ref UnitComponent unit, ref UnitStateComponent unitState)
        {
            foreach (var j in filterAllUnits)
            {
                ref var targetUnit = ref filterAllUnits.Get1(j);

                if (targetUnit.Team != unit.Team && 
                    Vector3.Distance(unit.transform.position, targetUnit.transform.position) <= unit.attackRange)
                {
                    AssignTarget(ref ent, ref unitState, ref filterAllUnits.GetEntity(j));
                    return;
                }
            }
        }

        private void AssignTarget(ref EcsEntity unit, ref UnitStateComponent state, ref EcsEntity target)
        {
            ref var targetComponent = ref unit.Get<HasTargetComponent>();
            targetComponent.target = target;

            state.PushState(ref unit, UnitState.Attacking);
        }
    }
}
