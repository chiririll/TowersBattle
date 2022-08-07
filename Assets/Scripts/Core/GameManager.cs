using TowersBattle.Data;
using UnityEngine;

namespace TowersBattle.Core
{
    /// <summary>
    /// Class for managing game states and storing level settings
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        private GameState state;
        [SerializeField] private LevelSettings levelSettings;

        private float prevTimeScale;

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
        /// Settings of the cuttent level
        /// </summary>
        public LevelSettings LevelSettings { get { return levelSettings; } }

        /// <summary>
        /// Current state of the game
        /// </summary>
        public GameState State { get { return state; } }

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

        private void Awake()
        {
            state = GameState.Playing;
            PushState(state);
        }
    }
}
