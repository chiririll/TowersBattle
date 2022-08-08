using TowersBattle.Core;
using TowersBattle.Data;
using UnityEngine;
using Zenject;

namespace TowersBattle.UI
{
    /// <summary>
    /// TODO
    /// </summary>
    public class PauseButton : MonoBehaviour 
    {
        [Inject] private GameManager gameManager;

        public void Click()
        {
            gameManager.PushState(GameState.Paused);
        }
    }
}
