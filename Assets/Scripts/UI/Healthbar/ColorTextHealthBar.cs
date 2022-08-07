using TMPro;
using TowersBattle.Data;
using UnityEngine;
using UnityEngine.UI;

namespace TowersBattle.UI
{
    /// <summary>
    /// TODO
    /// </summary>
    public class ColorTextHealthBar : BaseHealthBar
    {
        [Header("Colors")]
        [SerializeField] private Color playerColor;
        [SerializeField] private Color enemyColor;

        [Space(10)]
        [SerializeField] private Image healthBar;
        [SerializeField] private TMP_Text hp;
        private string text;

        public override void InitTeam(Team team)
        {
            healthBar.color = (team == Team.Player ? playerColor : enemyColor);
        }

        public override void UpdateHealth(int health)
        {
            var scale = healthBar.transform.localScale;
            healthBar.fillAmount = (float)health / maxHealth;

            if (hp != null)
                hp.text = text.Replace("#hp", health.ToString()).Replace("#max", maxHealth.ToString());
        }

        public override void DestroyHbar() {}

        private void Start()
        {
            if (hp != null)
                text = hp.text;
        }
    }
}
