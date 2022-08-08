using TMPro;
using TowersBattle.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TowersBattle.UI
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private Sprite defaultIcon;
        [SerializeField] private Image icon;
        [SerializeField] private TMP_Text label;

        private LevelSettings level;
        public LevelSettings Level {
            get { return level; }
            set 
            {
                level = value;
                UpdateButton();
            } 
        }

        public void UpdateButton()
        {
            icon.sprite = level.levelIcon != null ? level.levelIcon : defaultIcon;
            label.text = level.levelName;
        }

        public void Click()
        {
            SceneManager.LoadScene(level.sceneName);
        }
    }
}
