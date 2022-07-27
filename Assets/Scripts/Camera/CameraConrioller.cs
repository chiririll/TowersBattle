/*
 * MIT License
 * 
 * Copyright (c) 2022 Kirill Chizhov
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

namespace Assets.Scripts.Camera
{
    using UnityEngine;

    public class CameraConrioller : MonoBehaviour
    {
        [Header("References")]

        [SerializeField]
        private Camera mainCamera;
        [SerializeField]
        private Transform camHolder;
        [SerializeField]
        private SpriteRenderer map;

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
                this.dragOrigin = this.mainCamera.ScreenToWorldPoint(Input.mousePosition);
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
            Vector3 direction = this.dragOrigin - this.mainCamera.ScreenToWorldPoint(Input.mousePosition);
            this.camHolder.position += direction;

            this.UpdateBounds();
        }

        private void UpdateBounds()
        {
            Vector3 pos = this.camHolder.position;

            // Camera
            Vector3 camSize = this.mainCamera.ViewportToWorldPoint(new Vector3(1, 1, this.mainCamera.nearClipPlane)) - this.camHolder.position;

            Vector2 minBounds = this.map.bounds.min + camSize;
            Vector2 maxBounds = this.map.bounds.max - camSize;

            pos.x = Mathf.Clamp(pos.x, minBounds.x, maxBounds.x);
            pos.y = Mathf.Clamp(pos.y, minBounds.y, maxBounds.y);

            this.camHolder.position = pos;
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
            this.SetZoom(this.mainCamera.orthographicSize - zoomDelta);
        }

        // Set exact zoom value
        private void SetZoom(float value)
        {
            this.mainCamera.orthographicSize = Mathf.Clamp(value, this.zoomMin, this.zoomMax);
        }
    }
}
