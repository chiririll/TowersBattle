using TMPro;
using TowersBattle.Data;
using UnityEngine;
using UnityEngine.UI;

namespace TowersBattle.UI
{
    /// <summary>
    /// TODO
    /// </summary>
    public class ImageHealthBar : BaseHealthBar
    {
        [Header("Images")]
        [SerializeField] private Sprite playerSprite;
        [SerializeField] private Sprite enemySprite;

        [Space(10)]
        [SerializeField] private Image healthBar;
        [SerializeField] private TMP_Text hp;
        private string text;
        public override void InitTeam(Team team)
        {
            healthBar.sprite = team == Team.Player ? playerSprite : enemySprite;
        }

        public override void UpdateHealth(int health)
        {
            healthBar.fillAmount = (float)health / maxHealth;

            if (hp != null)
                hp.text = text.Replace("#hp", health.ToString()).Replace("#max", maxHealth.ToString());
        }

        public override void DestroyHbar() { }

        private void Start()
        {
            if (hp != null)
                text = hp.text;
        }
    }
}
