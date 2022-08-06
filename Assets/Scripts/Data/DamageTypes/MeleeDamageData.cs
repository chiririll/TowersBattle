namespace TowersBattle.Data
{
    using System;
    using UnityEngine;

    [Serializable]
    public class MeleeDamageData
    {
        [Min(0)] public int damage;
    }
}
