using Cinemachine;
using UnityEngine;

namespace TowersBattle
{
    /// <summary>
    /// TODO
    /// </summary>
    public class FreeCamZoom : MonoBehaviour 
    {
        /*[Header("References")]
        public Camera mainCamera;
        public CinemachineVirtualCamera freeCam;
        public Transform camTarget;

        [Header("Zoom")]
        [SerializeField] private float zoomTouchSens = 0.01f;
        // Zoom bounds
        [Min(1)]
        [SerializeField] private float zoomMin = 1f;
        [Min(1)]
        [SerializeField] private float zoomMax = 8f;

        void Update()
        {
            HandleMouseZoom();
            HandleTouchZoom();
        }

        void HandleMouseZoom()
        {
            ChangeZoom(Input.GetAxis("Mouse ScrollWheel"));
        }

        void HandleTouchZoom()
        {
            if (Input.touchCount != 2)
                return;

            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float curMagnitude = (touchZero.position - touchOne.position).magnitude;

            float diff = curMagnitude - prevMagnitude;
            ChangeZoom(diff * zoomTouchSens);
        }

        // Zoom in or out
        void ChangeZoom(float zoomDelta)
        {
            SetZoom(mainCamera.orthographicSize - zoomDelta);
        }

        // Set exact zoom value
        void SetZoom(float value)
        {
            mainCamera.orthographicSize = Mathf.Clamp(value, zoomMin, zoomMax);
        }*/
    }
}
