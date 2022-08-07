using UnityEngine;
using Zenject;

namespace TowersBattle.Core
{
    public class GameManagerInstaller : MonoInstaller
    {
        [SerializeField] private GameManager gameManager;

        public override void InstallBindings()
        {
            Container.Bind<GameManager>().FromInstance(gameManager).AsSingle();
        }
    }
}
