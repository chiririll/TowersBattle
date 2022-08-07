using UnityEngine;

namespace TowersBattle.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class SafeArea : MonoBehaviour 
    {
        private void Awake()
        {
            // Stealed from: https://youtu.be/cyDflP3RqT4
            var rectTransform = GetComponent<RectTransform>();
            var safeArea = Screen.safeArea;
            
            var anchorMin = safeArea.position;
            var anchorMax = anchorMin + safeArea.size;
            
            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;
        }
    }
}
