using Leopotam.Ecs;
using TowersBattle.Data;
using UnityEngine;

namespace TowersBattle.Ecs
{
    public struct UnitComponent
    {
        public UnitType type;

        // Unity components
        public Transform transform;
        
        public Vector3 attackRangeAnchor;
        public Vector3 hitboxAnchor;

        private Team team;
        public Team Team
        {
            get { return team; }
        }
        
        public void SwapTeam(ref EcsEntity ent, Team team)
        {
            // TODO: Refactor
            this.team = team;

            ref var teamEvent = ref ent.Get<SwapTeamEvent>();
            teamEvent.targetTeam = team;
        }
    }
}
