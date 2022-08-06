using Leopotam.Ecs;
using TowersBattle.Data;
using UnityEngine;

namespace TowersBattle.Ecs
{
    /// <summary>
    /// TODO
    /// </summary>
    public class TargetFindingSystem : IEcsRunSystem
    {
        private EcsFilter<UnitComponent, AttackComponent, UnitStateComponent>.Exclude<HasTargetComponent, DeadTag> filterNeedTarget;
        private EcsFilter<UnitComponent, HealthComponent>.Exclude<DeadTag> filterAllUnits;

        public void Run()
        {
            foreach (var i in filterNeedTarget)
            {
                ref var ent = ref filterNeedTarget.GetEntity(i);
                ref var unit = ref filterNeedTarget.Get1(i);
                ref var attack = ref filterNeedTarget.Get2(i);
                ref var unitState = ref filterNeedTarget.Get3(i);
                    
                FindTarget(ref ent, ref unit, ref attack, ref unitState);
            }
        }

        private void FindTarget(ref EcsEntity ent, ref UnitComponent unit, ref AttackComponent attack, ref UnitStateComponent unitState)
        {
            foreach (var j in filterAllUnits)
            {
                ref var targetUnit = ref filterAllUnits.Get1(j);

                if (targetUnit.Team != unit.Team && 
                    Vector3.Distance(unit.transform.position + unit.attackRangeAnchor, 
                        targetUnit.transform.position + targetUnit.hitboxAnchor) <= attack.attackRange)
                {
                    AssignTarget(ref ent, ref unitState, ref filterAllUnits.GetEntity(j));
                    return;
                }
            }
        }

        private void AssignTarget(ref EcsEntity unit, ref UnitStateComponent unitState, ref EcsEntity targetEnt)
        {
            ref var target = ref unit.Get<HasTargetComponent>();
            target.unitPreviousState = unitState.State;
            
            target.entity = targetEnt;
            target.type = targetEnt.Get<UnitComponent>().type;

            unitState.State = UnitState.Attacking;
        }
    }
}
