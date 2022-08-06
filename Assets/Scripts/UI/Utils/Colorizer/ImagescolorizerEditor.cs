using UnityEditor;
using UnityEngine;

namespace TowersBattle.UI
{
    /// <summary>
    /// TODO
    /// </summary>
    [CustomEditor(typeof(ImagesColorizer))]
    public class ImagescolorizerEditor : Editor 
    {	
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            ImagesColorizer colorizer = (ImagesColorizer)target;

            if (GUILayout.Button("Colorize"))
            {
                colorizer.Colorize();
            }
        }
    }
}
