using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowersBattle.UI
{
    /// <summary>
    /// TODO
    /// </summary>
    public class PlayButton : MonoBehaviour 
    {	
        public void Click()
        {
            SceneManager.LoadScene("Game");
        }
    }
}
