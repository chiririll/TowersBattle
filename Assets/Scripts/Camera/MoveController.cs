namespace TowersBattle.Camera
{
    using TowersBattle.Input;
    using UnityEngine;
    using UnityEngine.InputSystem;

    /// <summary>
    /// TODO
    /// </summary>
    public class MoveController : MonoBehaviour 
    {
        /// <summary>
        /// TODO
        /// </summary>
        [SerializeField] private Transform target;

        /// <summary>
        /// Input wrapper
        /// </summary>
        private GameInput gameInput;

        /// <summary>
        /// TODO
        /// </summary>
        private Camera mainCamera;

        /// <summary>
        /// TODO
        /// </summary>
        public void MoveCamera(InputAction.CallbackContext context)
        {
            Debug.Log(context.phase);
            transform.position = mainCamera.ScreenToWorldPoint(context.ReadValue<Vector2>());
        }    

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        private void OnEnable()
        {
            // gameInput.GameActions.CameraMovement.started += MoveCamera;
        }

        private void OnDisable()
        {
            // gameInput.GameActions.CameraMovement.started -= MoveCamera;
        }
    }
}
