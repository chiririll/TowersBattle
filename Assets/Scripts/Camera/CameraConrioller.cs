// <copyright file="CameraConrioller.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Assets.Scripts.Camera
{
    using UnityEngine;

    public class CameraConrioller : MonoBehaviour
    {
        [Header("References")]
        public Camera MainCamera;
        public Transform CamHolder;
        public SpriteRenderer Map;

        [Header("Zoom")]
        [SerializeField]
        private float touchZoomSensivity = 0.01f;

        // Zoom bounds
        [Min(1)]
        [SerializeField]
        private float zoomMin = 1f;

        [Min(1)]
        [SerializeField]
        private float zoomMax = 8f;

        // Drag bounds
        private Vector3 dragOrigin;

        private void Update()
        {
            // Updating start touch position
            if (Input.GetMouseButtonDown(0))
            {
                this.dragOrigin = this.MainCamera.ScreenToWorldPoint(Input.mousePosition);
            }

            // Handling camera drag direction based on start touch position
            if (Input.GetMouseButton(0) && Input.touchCount <= 1)
            {
                this.Drag();
            }

            this.HandleMouseZoom();
            this.HandleTouchZoom();

            this.UpdateBounds();
        }

        /* Drag */
        private void Drag()
        {
            Vector3 direction = this.dragOrigin - this.MainCamera.ScreenToWorldPoint(Input.mousePosition);
            this.CamHolder.position += direction;

            this.UpdateBounds();
        }

        private void UpdateBounds()
        {
            Vector3 pos = this.CamHolder.position;

            // Camera
            Vector3 camSize = this.MainCamera.ViewportToWorldPoint(new Vector3(1, 1, this.MainCamera.nearClipPlane)) - this.CamHolder.position;

            Vector2 minBounds = this.Map.bounds.min + camSize;
            Vector2 maxBounds = this.Map.bounds.max - camSize;

            pos.x = Mathf.Clamp(pos.x, minBounds.x, maxBounds.x);
            pos.y = Mathf.Clamp(pos.y, minBounds.y, maxBounds.y);

            this.CamHolder.position = pos;
        }

        /* Zoom */

        private void HandleMouseZoom()
        {
            this.ChangeZoom(Input.GetAxis("Mouse ScrollWheel"));
        }

        private void HandleTouchZoom()
        {
            if (Input.touchCount != 2)
            {
                return;
            }

            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float curMagnitude = (touchZero.position - touchOne.position).magnitude;

            float diff = curMagnitude - prevMagnitude;
            this.ChangeZoom(diff * this.touchZoomSensivity);
        }

        // Zoom in or out
        private void ChangeZoom(float zoomDelta)
        {
            this.SetZoom(this.MainCamera.orthographicSize - zoomDelta);
        }

        // Set exact zoom value
        private void SetZoom(float value)
        {
            this.MainCamera.orthographicSize = Mathf.Clamp(value, this.zoomMin, this.zoomMax);
        }
    }
}
