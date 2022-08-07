using UnityEditor;
using UnityEngine;

namespace TowersBattle.UI
{
    /// <summary>
    /// TODO
    /// </summary>
    [CustomEditor(typeof(Colorizer))]
    public class ImagescolorizerEditor : Editor 
    {	
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            Colorizer colorizer = (Colorizer)target;

            if (GUILayout.Button("Colorize"))
            {
                colorizer.Colorize();
            }
        }
    }
}
