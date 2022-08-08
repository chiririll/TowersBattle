using System.Collections.Generic;
using UnityEngine;

namespace TowersBattle.Data.Waves
{
    [System.Serializable]
    public class Wave
    {
        public Cooldown cooldown;
        public bool infinite;
        public List<UnitCount> units;

        public Wave() { }
        public Wave(Wave other) 
        {
            cooldown = other.cooldown;
            infinite = other.infinite;

            units = new List<UnitCount>();
            foreach (var unit in other.units)
            {
                units.Add(new UnitCount(unit));
            }
        }

        public Unit GetUnit()
        {
            while (units.Count > 0)
            {
                int index = Random.Range(0, units.Count);
                if (infinite)
                    return units[index].unit;

                if (units[index].count <= 0)
                {
                    units.RemoveAt(index);
                    continue;
                }

                units[index].count--;
                return units[index].unit;
            }
            return null;
        }
    }
}
