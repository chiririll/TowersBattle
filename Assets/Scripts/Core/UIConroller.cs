using System.Collections;
using TowersBattle.Data;
using UnityEngine;
using Zenject;

namespace TowersBattle.Core
{
    /// <summary>
    /// TODO
    /// </summary>
    public class UIConroller : MonoBehaviour 
    {
        

        [Inject] private GameManager gameManager;
        [SerializeField] private float endGameDelay; // TODO: move it somewhere

        [Space(10)]
        [SerializeField] private GameObject gameUI;
        [SerializeField] private GameObject pauseUI;
        [Space(10)]
        [SerializeField] private GameObject endGameUI;
        [SerializeField] private GameObject endGameWin;
        [SerializeField] private GameObject endGameLoose;

        private void StateChanged(GameState state, GameState lastState)
        {
            switch (state)
            {
                case GameState.Playing:
                case GameState.Paused:
                    EnableUI(state);
                    break;

                case GameState.Victory:
                case GameState.GameOver:
                    var endGameScreen = EnableEndgameUI(state);
                    StartCoroutine(endGameScreen);
                    break;
            }
        }

        private void DisableUI()
        {
            gameUI.SetActive(false);
            pauseUI.SetActive(false);
            endGameUI.SetActive(false);
        }

        private void EnableUI(GameState state)
        {
            DisableUI();

            switch (state)
            {
                case GameState.Playing:
                    gameUI.SetActive(true);
                    break;
                case GameState.Paused:
                    pauseUI.SetActive(true);
                    break;
                case GameState.Victory:
                case GameState.GameOver:
                    ShowEndGameResult(state);
                    break;
            }
        }

        private void ShowEndGameResult(GameState state)
        {
            endGameUI.SetActive(true);
            endGameWin.SetActive(false);
            endGameLoose.SetActive(false);

            switch (state)
            {
                case GameState.Victory:
                    endGameWin.SetActive(true);
                    break;
                case GameState.GameOver:
                    endGameLoose.SetActive(true);
                    break;
            }
        }

        private IEnumerator EnableEndgameUI(GameState state)
        {
            DisableUI();
            yield return new WaitForSeconds(endGameDelay);
            EnableUI(state);
        }

        private void OnEnable()
        {
            gameManager.GameStateChanged += StateChanged;
        }

        private void OnDisable()
        {
            gameManager.GameStateChanged -= StateChanged;
        }
    }
}

