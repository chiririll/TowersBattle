using TowersBattle.Data;
using UnityEngine;

namespace TowersBattle.Core
{
    /// <summary>
    /// TODO
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        private float prevTimeScale;
        private GameState state;

        /// <summary>
        /// Game state change event handler
        /// </summary>
        /// <param name="state">Current game state</param>
        /// <param name="lastState">Previous game state</param>
        public delegate void GameStateChangedEventHandler(GameState state, GameState lastState);

        /// <summary>
        /// Game state changed event
        /// </summary>
        public event GameStateChangedEventHandler GameStateChanged;
        
        /// <summary>
        /// Current state of the game
        /// </summary>
        public GameState State 
        { 
            get { return state; } 
            //set 
            //{
            //    GameStateChanged?.Invoke(value, state);
            //    state = value; 
            //}
        }

        /// <summary>
        /// Method for changing game state
        /// </summary>
        /// <param name="targetState">Target game state</param>
        public void PushState(GameState targetState)
        {
            switch (targetState)
            {
                case GameState.Playing:
                    if (state == GameState.Paused)
                        UnpauseGame();
                    break;
                case GameState.Paused:
                    if (state == GameState.Playing)
                        PauseGame();
                    break;
                case GameState.Victory:
                    break;
                case GameState.GameOver:
                    break;
            }

            GameStateChanged?.Invoke(targetState, state);
            state = targetState;
        }

        private void PauseGame()
        {
            prevTimeScale = Time.timeScale;
            Time.timeScale = 0;
        }

        private void UnpauseGame()
        {
            Time.timeScale = prevTimeScale;
        }
    }
}
