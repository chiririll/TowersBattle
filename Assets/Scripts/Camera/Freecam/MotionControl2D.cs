namespace TowersBattle.Camera.Freecam
{
    using UnityEngine;
    using UnityEngine.InputSystem;

    /// <summary>
    /// Class for controlling free camera movement
    /// </summary>
    public class MotionControl2D : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Transform targetTransform;
        [SerializeField] private float sens = 1f;

        private Camera mainCamera;

        private void Awake()
        {
            // Initialization
            mainCamera = Camera.main;
        }

        /// <summary>
        /// Method for moving camera by delta
        /// </summary>
        /// <param name="delta">Vector for moving camera</param>
        public void Move(InputAction.CallbackContext context)
        {
            // TODO
        }
    }
}
