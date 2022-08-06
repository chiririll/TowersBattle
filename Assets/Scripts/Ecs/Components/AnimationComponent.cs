using Spine.Unity;
using System;
using TowersBattle.Data;
using UnityEngine;

namespace TowersBattle.Ecs
{
    [Serializable]
    public struct AnimationComponent
    {
        [Serializable]
        public struct Animation
        {
            public UnitState state;
            public string name;
            public bool loop;
            [Min(0)] public int track;
            [Min(1)] public int clipsCount;

            public string GetName(int clip) 
            {  
                if (clipsCount <= 1)
                    return name;

                return name + "_" + Mathf.Clamp(clip, 1, clipsCount).ToString();
            }
        }

        public Animation[] animations;
        [HideInInspector] public SkeletonAnimation animator;
        [HideInInspector] public int lastClip;
    }
}
