using Leopotam.Ecs;
using TowersBattle.Data;
using UnityEngine;

namespace TowersBattle.Ecs
{
    /// <summary>
    /// TODO
    /// </summary>
    public class SoundSystem : IEcsRunSystem
    {
        private EcsFilter<UnitStateChangedEvent, SoundComponent> stateEventFilter;
        private EcsFilter<AttackEvent, SoundComponent>.Exclude<DeadTag> attackEventFilter;

        public void Run()
        {
            foreach (var i in stateEventFilter)
            {
                ref var state = ref stateEventFilter.Get1(i);
                ref var soundComp = ref stateEventFilter.Get2(i);

                if (state.currentState == UnitState.Attacking)
                    continue;

                FindSound(ref soundComp, state.currentState);
            }

            foreach (var i in attackEventFilter)
            {
                FindSound(ref attackEventFilter.Get2(i), UnitState.Attacking);
            }
        }

        private void FindSound(ref SoundComponent soundComp, UnitState state)
        {
            foreach (var sound in soundComp.sounds)
            {
                if (sound.state != state)
                    continue;

                // Playing sound
                PlaySound(ref soundComp.soundSource, sound);
                return;
            }
        }

        private void PlaySound(ref AudioSource source, SoundComponent.Sound sound)
        {
            source.clip = sound.GetClip();
            source.loop = sound.loop;

            source.Play();
        }
    }
}
