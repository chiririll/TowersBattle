using System.Collections;
using TMPro;
using TowersBattle.Core;
using TowersBattle.Data;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace TowersBattle.UI
{
    /// <summary>
    /// TODO
    /// </summary>
    public class CooldownVisualizer : MonoBehaviour
    {
        [Inject] private InputManager inputManager;
        [Inject] private CoroutineManager coroutineManager;

        [SerializeField] private Button button;
        [SerializeField] private Image cooldownBar;
        [SerializeField] private TMP_Text countdown;

        private IEnumerator cooldownCoroutine;

        private IEnumerator Cooldown(float cooldownTime)
        {
            float remainTime = cooldownTime;

            SetCooldownState(true);

            while (remainTime > 0)
            {
                cooldownBar.fillAmount = remainTime / cooldownTime;
                

                if (remainTime >= 1f)
                    countdown.text = Mathf.Ceil(remainTime).ToString();
                else
                    countdown.text = remainTime.ToString("F1").Replace(",", ".");

                remainTime -= Time.deltaTime;
                yield return null;
            }

            SetCooldownState(false);
        }

        private void SetCooldownState(bool isEnabled)
        {
            button.interactable = !isEnabled;

            countdown.enabled = isEnabled;
            cooldownBar.enabled = isEnabled;
        }

        private void StartCooldown(Unit uinit, float cooldown)
        {
            cooldownCoroutine = Cooldown(cooldown);
            coroutineManager.StartCoroutine(cooldownCoroutine);
        }

        private void OnEnable()
        {
            inputManager.PlayerSpawnUnitEvent += StartCooldown;
        }

        private void OnDisable()
        {
            inputManager.PlayerSpawnUnitEvent -= StartCooldown;
        }

        private void Start()
        {
            SetCooldownState(false);
        }

    }
}
