namespace TowersBattle.Input
{
    using System.Collections;
    using UnityEngine;

    /// <summary>
    /// 
    /// </summary>
    public class PinchDetection : MonoBehaviour
    {
        private GameInput gameInput;
        private TouchControls controls;

        private Coroutine zoomCoroutine;

        private void Awake()
        {
            controls = new TouchControls();
        }

        private void OnEnable()
        {
            controls.Enable();
        }

        private void OnDisable()
        {
            controls.Disable();
        }

        private void Start()
        {
            controls.Pinch.SecondaryTouchContact.started += _ => ZoomStart();
            controls.Pinch.SecondaryTouchContact.canceled += _ => ZoomEnd();
        }

        /// <summary>
        /// Hook for starting zoom for startig zoom coroutine 
        /// </summary>
        private void ZoomStart()
        {
            zoomCoroutine = StartCoroutine(ZoomDetection());
        }

        /// <summary>
        /// Same as ZoomStart but stopping
        /// </summary>
        private void ZoomEnd()
        {
            StopCoroutine(zoomCoroutine);
        }

        /// <summary>
        /// Coroutine for calculating zoom value
        /// </summary
        IEnumerator ZoomDetection()
        {
            float prevDistance = 0f;
            float distance = 0f;
            while (true)
            {
                distance = Vector2.Distance(
                    controls.Pinch.PrimaryFingerPosition.ReadValue<Vector2>(),
                    controls.Pinch.SecondaryFingerPosition.ReadValue<Vector2>()
                );

                float delta = prevDistance - distance;

                prevDistance = distance;
            }
        }
    }
}
