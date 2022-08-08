using TowersBattle.Data;
using UnityEngine;

namespace TowersBattle.Ecs
{
    [System.Serializable]
    public struct SoundComponent
    {
        [System.Serializable]
        public struct Sound
        {
            public UnitState state;
            public bool loop;

            [SerializeField] private AudioClip[] sounds;

            public AudioClip GetClip()
            {
                return sounds[Random.Range(0, sounds.Length)];
            }
        }

        public Sound[] sounds;
        [HideInInspector] public AudioSource soundSource;
    }
}
