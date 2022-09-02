using Roguelike.Core.EventHandlers;
using Roguelike.Core.EventSubscribers;
using Roguelike.Loaders;
using Zenject;

namespace Roguelike.Installers.Scene
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