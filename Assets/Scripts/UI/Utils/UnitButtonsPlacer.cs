using TowersBattle.Core;
using TowersBattle.Data;
using UnityEngine;
using Zenject;

namespace TowersBattle.UI
{
    /// <summary>
    /// TODO
    /// </summary>
    public class UnitButtonsPlacer : MonoBehaviour 
    {
        [Inject] private GameManager gameManager;
        [Inject] private DiContainer diContainer;

        [SerializeField] private UnitSpawnButton buttonPrefab;
        [SerializeField] private float offset;

        private void PlaceUnitButton (Unit unit, int index)
        {
            GameObject btnGO = diContainer.InstantiatePrefab(buttonPrefab, transform);
            UnitSpawnButton btn = btnGO.GetComponent<UnitSpawnButton>();
            RectTransform btnRT = btnGO.GetComponent<RectTransform>();

            btn.Unit = unit;
            btnRT.anchoredPosition = new Vector3(btnRT.sizeDelta.x * index + offset * index, btnRT.anchoredPosition.y, btnRT.anchoredPosition.x);    
        }

        private void Start()
        {
            int index = 0;
            foreach (Unit unit in gameManager.LevelSettings.playerUnits.units)
            {
                PlaceUnitButton (unit, index);
                index++;
            }
        }
    }
}
