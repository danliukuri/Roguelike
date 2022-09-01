using Roguelike.Core.EventHandlers;
using Roguelike.Core.EventSubscribers;
using Roguelike.Core.Information;
using Roguelike.Loaders;
using UnityEngine;
using Zenject;

namespace Roguelike.Installers.Scene
{
    public class LevelInstaller : MonoInstaller
    {
        #region Fields
        [SerializeField] LevelSettings levelSettings;
        #endregion
        
        #region Methods
        public override void InstallBindings()
        {
            BindLevelSettings();
            BindLoader();
            BindEventHandler();
            BindEventSubscriber();
        }
        void BindLevelSettings()
        {
            Container.BindInstance(levelSettings);
            Container
                .Bind<LevelSettingsUpdater>()
                .FromNew()
                .AsSingle();
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