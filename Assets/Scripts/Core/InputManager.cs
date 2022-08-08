using TowersBattle.Data;
using UnityEngine;
using Zenject;

namespace TowersBattle.Core
{
    /// <summary>
    /// Class for managing players input actions related to gameplay
    /// </summary>
    public class InputManager : MonoBehaviour 
    {
        [Inject] private GameManager gameManager;
        private float nextSpawnTime;

        /// <summary>
        /// Player sapwn unit callback
        /// </summary>
        /// <param name="unit">Unit, that player wants to spawn</param>
        /// <param name="cooldown">Next iunit spawn interval</param>
        public delegate void PlayerInputSpawnUnitEvent(Unit unit, float cooldown);

        /// <summary>
        /// Event called when player want to spawn unit
        /// </summary>
        public event PlayerInputSpawnUnitEvent PlayerSpawnUnitEvent;
        
        /// <summary>
        /// Method for sending player spawn event to game
        /// </summary>
        /// <param name="unit">Players unit choice</param>
        public void SpawnUnit(Unit unit)
        {
            if (gameManager.State != GameState.Playing)
                return;

            if (nextSpawnTime > Time.time)
                return;

            nextSpawnTime = Time.time + gameManager.LevelSettings.playerCooldown.Get();
            PlayerSpawnUnitEvent?.Invoke(unit, gameManager.LevelSettings.playerCooldown.Get());
        }
    }
}
