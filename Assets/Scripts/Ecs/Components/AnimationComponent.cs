namespace TowersBattle.Ecs
{
    using Spine.Unity;
    using System;
    using UnityEngine;

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
    }
}
