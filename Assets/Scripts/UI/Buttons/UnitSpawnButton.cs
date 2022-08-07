using TowersBattle.Core;
using TowersBattle.Data;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TowersBattle.UI
{
    /// <summary>
    /// TODO
    /// </summary>
    public class UnitSpawnButton : MonoBehaviour 
    {
        [SerializeField] private Unit unit;
        [SerializeField] private Image icon;

        [Inject] private InputManager inputManager;

        public Unit Unit 
        {
            get 
            { 
                return unit; 
            }
            set 
            { 
                unit = value; 
                UpdateIcon();
            }
        }

        public void Click()
        {
            inputManager.SpawnUnit(unit);
        }

        private void UpdateIcon()
        {
            if (unit != null)
                icon.sprite = unit.icon;
        }

        private void Awake()
        {
            UpdateIcon();
        }
    }
}
