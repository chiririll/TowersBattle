namespace TowersBattle.Ecs
{
    using UnityEngine;
    using Leopotam.Ecs;
    using TowersBattle.Data;

    /// <summary>
    /// Class for starting and running ecs
    /// </summary>
    public class EcsStartup : MonoBehaviour 
    {
        private EcsWorld world;
        private EcsSystems systems;

        private SpawnTable spawnTable;


        /// <summary>
        /// TODO
        /// </summary>
        private void AddSystems()
        {

        }   
        
        /// <summary>
        /// TODO
        /// </summary>
        private void AddInjections()
        {
            
        }

        /// <summary>
        /// TODO
        /// </summary>
        private void AddOneframe()
        {

        }

        private void Start()
        {
            world = new EcsWorld();
            systems = new EcsSystems(world);

            AddInjections();
            AddSystems();
            // AddOneframe();

            systems.Init();
        }

        private void Update()
        {
            systems.Run();
        }

        private void OnDestroy()
        {
            systems?.Destroy();
            systems = null;

            world?.Destroy();
            world = null;
        }
    }
}
