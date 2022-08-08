using TowersBattle.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace TowersBattle.UI
{
    /// <summary>
    /// TODO
    /// </summary>
    public class MainMenuButton : MonoBehaviour 
    {
        [Inject] private GameManager gameManager;

        public void Click()
        {
            if (gameManager.State == Data.GameState.Paused)
                gameManager.PushState(Data.GameState.Playing);
            
            SceneManager.LoadScene("MainMenu");
        }
    }
}
