using UnityEngine;
using TMPro;

namespace TowersBattle
{
    /// <summary>
    /// TODO
    /// </summary>
    public class FpsCounter : MonoBehaviour 
    {
        [SerializeField] private TMP_Text textComponent;
        string fpsText;

        private void Awake()
        {
            fpsText = textComponent.text;
        }

        private void Update()
        {
            textComponent.text = fpsText.Replace("###", Mathf.Floor(1 / Time.unscaledDeltaTime).ToString());
        }
    }
}
