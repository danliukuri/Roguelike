using Roguelike.UI.EventHandlers;
using Roguelike.UI.EventSubscribers;
using Roguelike.UI.Information;
using UnityEngine;
using Zenject;

namespace Roguelike.Installers.Scene
{
    public class MenusInstaller: MonoInstaller
    {
        #region Fields
        [SerializeField] GameObject mainMenu, playerDeathMenu;
        #endregion
        
        #region Methods
        public override void InstallBindings()
        {
            BindMenusInfo();
            BindPlayerDeathMenuEventSubscriber();
            BindPlayerDeathMenuEventHandler();
        }
        void BindMenusInfo()
        {
            Container
                .Bind<MenusInfo>()
                .FromNew()
                .AsSingle()
                .WithArguments(mainMenu, playerDeathMenu);
        }
        void BindPlayerDeathMenuEventSubscriber()
        {
            Container
                .BindInterfacesAndSelfTo<PlayerDeathMenuEventSubscriber>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }
        void BindPlayerDeathMenuEventHandler()
        {
            Container
                .Bind<PlayerDeathMenuEventHandler>()
                .FromComponentOn(playerDeathMenu)
                .AsSingle();
        }
        #endregion
    }
}