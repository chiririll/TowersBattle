using UnityEngine;

namespace TowersBattle.UI
{
    /// <summary>
    /// TODO
    /// </summary>
    public class PlayButton : MonoBehaviour 
    {
        [SerializeField] private GameObject menuUI;
        [SerializeField] private GameObject levelsUI;

        public void Click()
        {
            menuUI.SetActive(false);
            levelsUI.SetActive(true);
        }
    }
}
