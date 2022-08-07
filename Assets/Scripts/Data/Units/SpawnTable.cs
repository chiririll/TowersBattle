namespace TowersBattle.Data
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "New Unit table", menuName = "Units/Unit table")]
    public class SpawnTable : ScriptableObject 
    {
        public Unit[] units;

        public Unit GetRandomUnit()
        {
            return units[Random.Range(0, units.Length)];
        }
    }
}
