using Leopotam.Ecs;
using TowersBattle.Data;
using UnityEngine;

namespace TowersBattle.Ecs
{
    public class SwapTeamSystem : IEcsRunSystem
    {
        private EcsFilter<SwapTeamEvent, AnimationComponent> filer;

        public void Run()
        {
            foreach (var i in filer)
            {
                ref var swapEvent = ref filer.Get1(i);
                ref var animator = ref filer.Get2(i);

                animator.animator.skeleton.SetSkin(swapEvent.targetTeam == Team.Player ? "1" : "2");
                animator.animator.skeleton.ScaleX = Mathf.Abs(animator.animator.skeleton.ScaleX) 
                    * (swapEvent.targetTeam == Team.Player ? 1 : -1);
            }
        }
    }
}
