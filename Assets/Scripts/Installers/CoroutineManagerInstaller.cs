using UnityEngine;
using Zenject;

namespace TowersBattle.Core
{
    public class CoroutineManagerInstaller : MonoInstaller
    {
        [SerializeField] private CoroutineManager coroutineManager;
        public override void InstallBindings()
        {
            Container.Bind<CoroutineManager>().FromInstance(coroutineManager).AsSingle();
        }
    }
}
