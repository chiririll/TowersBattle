namespace TowersBattle.Camera.Freecam
{
    using UnityEngine;
    using Cinemachine;

    /// <summary>
    /// Class for controlling free camera zooming
    /// </summary>
    public class ZoomControl : MonoBehaviour 
    {
        [Header("Zoom")]
        [Min(0f)]
        [SerializeField]
        private float zoomMin = 0f;
        [Min(0f)]
        [SerializeField]
        private float zoomMax = 10f;

        [Header("Components")]
        [SerializeField] private CinemachineVirtualCamera freeCam;

        /// <summary>
        /// Method for zooming camera (Bounded by zoomMin and zoomMax)
        /// </summary>
        /// <param name="delta">Zoom delta value</param>
        public void Zoom(float delta)
        {
            // TODO: handle input
            freeCam.m_Lens.OrthographicSize = Mathf.Clamp(freeCam.m_Lens.OrthographicSize + delta, zoomMin, zoomMax);
        }
    }
}
