using Roguelike.UI.EventHandlers;
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