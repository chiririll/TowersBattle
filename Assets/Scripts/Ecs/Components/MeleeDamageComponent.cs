using System;
using UnityEngine;

namespace TowersBattle.Ecs
{
    [Serializable]
    public struct MeleeDamageComponent
    {
        [Min(0)] public int damage;
    }
}
