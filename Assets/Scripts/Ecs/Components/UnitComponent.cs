using Leopotam.Ecs;
using TowersBattle.Data;
using UnityEngine;

namespace TowersBattle.Ecs
{
    public struct UnitComponent
    {
        // Unit type
        public UnitType type;

        // Unity components
        public Transform transform;
        
        // Offsets
        public Vector3 attackRangeAnchor;
        public Vector3 hitboxAnchor;

        // Team
        private Team team;
        public Team Team
        {
            get { return team; }
        }
        
        public void SwapTeam(ref EcsEntity ent, Team team, bool callEvent = true)
        {
            this.team = team;

            if (!callEvent)
                return;

            ref var teamEvent = ref ent.Get<SwapTeamEvent>();
            teamEvent.targetTeam = team;
        }
    }
}
