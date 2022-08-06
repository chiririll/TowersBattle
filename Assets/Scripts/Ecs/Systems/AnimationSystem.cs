using Leopotam.Ecs;
using TowersBattle.Data;

namespace TowersBattle.Ecs
{
    /// <summary>
    /// TODO
    /// </summary>
    public class AnimationSystem : IEcsRunSystem
    {
        private EcsFilter<UpdateAnimationEvent, AnimationComponent> animationEventFilter;
        private EcsFilter<UnitStateChangedEvent, AnimationComponent> stateEventFilter;
        private EcsFilter<AttackEvent, AnimationComponent>.Exclude<DeadTag> attackEventFilter;

        public void Run()
        {
            foreach (var i in animationEventFilter)
            {
                ref var animEvent = ref animationEventFilter.Get1(i);
                ref var animator = ref animationEventFilter.Get2(i);
                animator.lastClip = animEvent.clip;
                
                SetAnimation(ref animator, animEvent.state);
            }

            foreach (var i in stateEventFilter)
            {
                ref var stateEvent = ref stateEventFilter.Get1(i);
                SetAnimation(ref stateEventFilter.Get2(i), stateEvent.currentState);
            }    

            foreach(var i in attackEventFilter)
            {
                SetAnimation(ref attackEventFilter.Get2(i), UnitState.Attacking);
            }
        }

        private void SetAnimation(ref AnimationComponent animator, UnitState state)
        {
            foreach (var anim in animator.animations)
            {
                if (anim.state == state)
                {
                    if (anim.track == 0)
                        animator.animator.AnimationState.ClearTracks();

                    animator.animator.AnimationState.SetAnimation(anim.track, anim.GetName(animator.lastClip), anim.loop);
                    return;
                }
                    
            }
        }
    }
}
