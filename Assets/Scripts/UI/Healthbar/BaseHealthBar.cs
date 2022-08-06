using TowersBattle.Ecs;
using UnityEngine;

namespace TowersBattle.UI
{
    /// <summary>
    /// Base class for healtbar controller
    /// </summary>
    public abstract class BaseHealthBar : MonoBehaviour
    {
        protected int maxHealth;

        /// <summary>
        /// Max health value initializer
        /// </summary>
        /// <param name="maxHealth">Max health value</param>
        public void InitMaxHealth(int maxHealth)
        {
            this.maxHealth = maxHealth;
        }    

        /// <summary>
        /// Health bar team initializer
        /// </summary>
        /// <param name="team">Unit team</param>
        public abstract void InitTeam(Team team);

        /// <summary>
        /// Callback method for updating healthbar (when health changed)
        /// </summary>
        /// <param name="health">Updated health value</param>
        public abstract void UpdateHealth(int health);
    }
}
