using Roguelike.Core.Gameplay.Events.Handling;
using Roguelike.Core.Gameplay.Events.Subscribing;
using Roguelike.Setup.Loading;
using Zenject;

namespace Roguelike.Setup.Installation.Scene
{
    public class LevelInstaller : MonoInstaller
    {
        #region Methods
        public override void InstallBindings()
        {
            BindLoader();
            BindEventHandler();
            BindEventSubscriber();
        }
        void BindLoader()
        {
            Container
                .BindInterfacesAndSelfTo<LevelLoader>()
                .FromNew()
                .AsSingle();
        }
        void BindEventHandler()
        {
            Container
                .BindInterfacesAndSelfTo<LevelEventHandler>()
                .FromNew()
                .AsSingle();
        }
        void BindEventSubscriber()
        {
            Container
                .BindInterfacesAndSelfTo<LevelEventSubscriber>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }
        #endregion
    }
}