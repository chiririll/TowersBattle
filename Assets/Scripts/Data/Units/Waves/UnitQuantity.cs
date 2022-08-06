using System;
using UnityEngine;

namespace TowersBattle.Data
{
    [Serializable]
    public class UnitQuantity
    {
        public Unit unit;
        [Min(0)] public int count;
    }
}
