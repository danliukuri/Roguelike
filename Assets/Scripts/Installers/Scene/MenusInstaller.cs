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
        }
        void BindMenusInfo()
        {
            Container
                .Bind<MenusInfo>()
                .FromNew()
                .AsSingle()
                .WithArguments(mainMenu, playerDeathMenu);
        }
        #endregion
    }
}