using TowersBattle.Data;
using UnityEngine;
using Zenject;

namespace TowersBattle.UI
{
    /// <summary>
    /// TODO
    /// </summary>
    public class LevelButtonsPlacer : MonoBehaviour 
    {
        [Inject] private DiContainer diContainer;

        [SerializeField] private LevelSettings[] levels;
        [SerializeField] private float offset;
        [SerializeField] private GameObject button;

        private void PlaceButton(int index)
        {
            GameObject btnGO = Instantiate(button, transform);
            LevelButton levelButton = btnGO.GetComponent<LevelButton>();
            RectTransform btnRT = btnGO.GetComponent<RectTransform>();

            levelButton.Level = levels[index];
            btnRT.anchoredPosition = new Vector3(btnRT.sizeDelta.x * index + offset * index, btnRT.anchoredPosition.y, btnRT.anchoredPosition.x);

            btnGO.SetActive(true);
        }

        private void Start()
        {
            for (int i = 0; i < levels.Length; i++)
                PlaceButton(i);
        }
    }
}
