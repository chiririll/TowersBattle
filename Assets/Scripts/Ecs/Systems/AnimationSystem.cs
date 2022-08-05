namespace TowersBattle.Ecs
{
    using UnityEngine;
    using Leopotam.Ecs;

    /// <summary>
    /// TODO
    /// </summary>
    public class AnimationSystem : IEcsRunSystem
    {
        private EcsFilter<UpdateAnimationEvent, AnimationComponent> filter;

        private AnimationComponent.Animation defaultAnimation;

        public AnimationSystem()
        {
            defaultAnimation.name = "";
        }

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var animEvent = ref filter.Get1(i);
                ref var animator = ref filter.Get2(i);

                var anim = FindAnimation(ref animator, animEvent.state);
                if (anim.name == "")
                    continue;

                animator.animator.AnimationState.SetAnimation(0, anim.GetName(animEvent.clip), anim.loop);
            }
        }

        public AnimationComponent.Animation FindAnimation(ref AnimationComponent animator, UnitState state)
        {
            foreach (var anim in animator.animations)
            {
                if (anim.state.Equals(state))
                    return anim;
            }

            return defaultAnimation;
        }
    }
}
