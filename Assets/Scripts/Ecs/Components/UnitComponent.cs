namespace TowersBattle.Ecs
{
    using Leopotam.Ecs;
    using UnityEngine;

    public struct UnitComponent
    {
        // Unity components
        public Transform transform;
        public Vector3 attackRangeAnchor;
        public Vector3 hitboxAnchor;

        // Combat
        public float attackRange;
        public float attackSpeed;
        private Team team;
        public Team Team
        {
            get { return team; }
        }
        

        public void SwapTeam(ref EcsEntity ent, Team team)
        {
            this.team = team;

            ref var teamEvent = ref ent.Get<SwapTeamEvent>();
            teamEvent.targetTeam = team;
        }
    }
}
