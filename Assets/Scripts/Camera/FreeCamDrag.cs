using System;
using System.Collections;
using TowersBattle.Core;
using TowersBattle.Data;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace TowersBattle
{
    /// <summary>
    /// TODO
    /// </summary>
    public class FreeCamDrag : MonoBehaviour 
    {
        // TODO: split script
        [Header("References")]
        [SerializeField] private PlayerInput input;
        [SerializeField] private Transform camTarget;

        [Inject] private GameManager gameManager;
        private Camera mainCamera;

        private Coroutine dragCoroutine;

        public void Drag(InputAction.CallbackContext context)
        {
            if (gameManager.State != GameState.Playing)
                return;

            if (context.performed)
                dragCoroutine = StartCoroutine(Dragging());

            if (context.canceled)
                StopCoroutine(dragCoroutine);
        }


        private IEnumerator Dragging()
        {
            camTarget.position = mainCamera.transform.position;

            var dragAction = input.actions["InitPosition"];
            Vector3 dragOrigin = mainCamera.ScreenToWorldPoint(dragAction.ReadValue<Vector2>());
            
            while (true)
            {
                Vector3 direction = dragOrigin - mainCamera.ScreenToWorldPoint(dragAction.ReadValue<Vector2>());
                camTarget.position = new Vector3(camTarget.position.x + direction.x, camTarget.position.y + direction.y, camTarget.position.z);
                
                yield return null;
            }
        }

        private void StateChanged(GameState state, GameState lastState)
        {
            if (state != GameState.Playing && dragCoroutine != null)
                StopCoroutine(dragCoroutine);
        }

        private void Start()
        {
            mainCamera = Camera.main;
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
