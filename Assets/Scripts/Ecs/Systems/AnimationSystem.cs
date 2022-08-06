using Leopotam.Ecs;

namespace TowersBattle.Ecs
{
    /// <summary>
    /// TODO
    /// </summary>
    public class AnimationSystem : IEcsRunSystem
    {
        private EcsFilter<UpdateAnimationEvent, AnimationComponent> animationEventFilter;
        private EcsFilter<UnitStateChangedEvent, AnimationComponent> stateEventFilter;

        public void Run()
        {
            foreach (var i in animationEventFilter)
            {
                ref var animEvent = ref animationEventFilter.Get1(i);
                SetAnimation(ref animationEventFilter.Get2(i), animEvent.state, animEvent.clip);
            }

            foreach (var i in stateEventFilter)
            {
                ref var stateEvent = ref stateEventFilter.Get1(i);
                SetAnimation(ref stateEventFilter.Get2(i), stateEvent.currentState, 0);
            }    
        }

        private void SetAnimation(ref AnimationComponent animator, UnitState state, int clip)
        {
            foreach (var anim in animator.animations)
            {
                if (anim.state == state)
                {
                    animator.animator.AnimationState.SetAnimation(anim.track, anim.GetName(clip), anim.loop);
                    return;
                }
                    
            }
        }
    }
}
