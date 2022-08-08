using System;
using UnityEngine;

namespace TowersBattle.Data.Waves
{
    [Serializable]
    public class UnitCount
    {
        public Unit unit;
        [Min(0)] public int count;

        public UnitCount() { }
        public UnitCount(UnitCount other) 
        {
            unit = other.unit;
            count = other.count;
        }
    }
}
