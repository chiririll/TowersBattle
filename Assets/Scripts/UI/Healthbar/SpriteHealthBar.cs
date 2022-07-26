using TowersBattle.Data;
using UnityEngine;

namespace TowersBattle.UI
{
    /// <summary>
    /// Simple healthbar
    /// </summary>
    public class SpriteHealthBar : BaseHealthBar
    {
        [Header("Colors")]
        [SerializeField] private Color playerColor;
        [SerializeField] private Color enemyColor;

        [Space(10)]
        [SerializeField] private SpriteRenderer healthBar;

        public override void InitTeam(Team team)
        {
            healthBar.color = (team == Team.Player ? playerColor : enemyColor);
        }

        public override void UpdateHealth(int health)
        {
            var scale = healthBar.transform.localScale;
            healthBar.transform.localScale = new Vector3((float)health / maxHealth, scale.y, scale.z);
        }
    }
}
