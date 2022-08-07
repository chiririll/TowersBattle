using System;
using TowersBattle.Data;
using UnityEngine;
using Zenject;

namespace TowersBattle.Core
{
    /// <summary>
    /// TODO
    /// </summary>
    public class CameraControlller : MonoBehaviour
    {
        private enum Cam
        {
            Free,
            Player,
            Enemy
        }

        [Inject] private GameManager gameManager;

        [SerializeField] private GameObject freeCamera;
        [SerializeField] private GameObject playerCamera;
        [SerializeField] private GameObject enemyCamera;

        private void StateChanged(GameState state, GameState lastState)
        {
            switch (state)
            {
                case GameState.Victory:
                    SetCamera(Cam.Enemy);
                    break;
                case GameState.GameOver:
                    SetCamera(Cam.Player);
                    break;
            }
        }

        private void SetCamera(Cam cam)
        {
            freeCamera.SetActive(false);
            playerCamera.SetActive(false);
            enemyCamera.SetActive(false);

            switch (cam)
            {
                case Cam.Free:
                    freeCamera.SetActive(true);
                    break;
                case Cam.Player:
                    playerCamera.SetActive(true);
                    break;
                case Cam.Enemy:
                    enemyCamera.SetActive(true);
                    break;
            }
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
