using System;
using UnityEngine;
using UnityEngine.UI;

namespace TowersBattle.UI
{
    /// <summary>
    /// TODO
    /// </summary>
    public class Colorizer : MonoBehaviour
    {
        [Serializable]
        public class ColorItem
        {
            public Color color;
            
            public Image[] images;
            public Button[] buttonss;
        }

        [SerializeField] private ColorItem[] items;

        [ExecuteInEditMode]
        public void Colorize()
        {
            foreach (var item in items)
            {
                foreach (var image in item.images)
                    image.color = item.color;

                foreach (var button in item.buttonss)
                {
                    // if (buttonPrefab)
                }
            }
        }

        private void Start()
        {
            // TODO: load color from settings
            Colorize();
        }
    }
}
