using TowersBattle.Core;
using TowersBattle.Data;
using UnityEngine;
using Zenject;

namespace TowersBattle
{
    /// <summary>
    /// TODO
    /// </summary>
    public class ResumeButton : MonoBehaviour 
    {
        [Inject] private GameManager gameManager;

        public void Click()
        {
            gameManager.PushState(GameState.Playing);
        }
    }
}
