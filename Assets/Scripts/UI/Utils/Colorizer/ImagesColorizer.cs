using UnityEngine;
using UnityEngine.UI;

namespace TowersBattle.UI
{
    /// <summary>
    /// TODO
    /// </summary>
    public class ImagesColorizer : MonoBehaviour
    {
        [SerializeField] private Color color;
        [SerializeField] private Image[] images;

        // Todo: add change color event

        [ExecuteInEditMode]
        public void Colorize()
        {
            foreach (var image in images)
            {
                image.color = color;
            }
        }

        private void Start()
        {
            // TODO: load color from settings
            Colorize();
        }
    }
}
