using Roguelike.Core.EventHandlers;
using Roguelike.Core.EventSubscribers;
using Roguelike.Core.Information;
using UnityEngine;
using Zenject;

namespace Roguelike.Installers.Bootstrap
{
    public class LevelInstaller : MonoInstaller
    {
        #region Fields
        [SerializeField] LevelSettings levelSettings;
        #endregion

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
                .AsSingle()
                .WithArguments(levelSettings);
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