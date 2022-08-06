using UnityEngine;

namespace TowersBattle.Settings
{
    public class GameSettings : MonoBehaviour
    {
        public SoundSettings soundSettings;
        public UiSettings uiSettings;

        public void Load()
        {
            // TODO
        }

        public void Save()
        {
            // TODO
        }

        private void Awake()
        {
            Load();
        }

        private void OnDestroy()
        {
            Save();
        }
    }
}
