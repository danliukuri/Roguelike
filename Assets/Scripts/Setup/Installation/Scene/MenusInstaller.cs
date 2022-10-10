using Roguelike.UI.Events.Handling;
using Roguelike.UI.Events.Subscribing;
using Roguelike.UI.Information;
using TMPro;
using UnityEngine;
using Zenject;

namespace Roguelike.Setup.Installation.Scene
{
    public class MenusInstaller: MonoInstaller
    {
        #region Fields
        [Header("Menus:")]
        [SerializeField] GameObject mainMenu;
        [SerializeField] GameObject playerDeathMenu;
        [SerializeField] GameObject gameplayMenu;
        [Header("Gameplay menu:")]
        [SerializeField] TMP_Text levelNumberTMP;
        [SerializeField] TMP_Text keysNumberTMP;
        #endregion
        
        #region Methods
        public override void InstallBindings()
        {
            BindMenusInfo();
            BindPlayerDeathMenuEventSubscriber();
            BindPlayerDeathMenuEventHandler();
            BindGameplayMenuEventSubscriber();
            BindGameplayMenuEventHandler();
        }
        void BindMenusInfo()
        {
            Container
                .Bind<MenusInfo>()
                .FromNew()
                .AsSingle()
                .WithArguments(mainMenu, playerDeathMenu, gameplayMenu);
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
        void BindGameplayMenuEventSubscriber()
        {
            Container
                .BindInterfacesAndSelfTo<GameplayMenuEventSubscriber>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }
        void BindGameplayMenuEventHandler()
        {
            Container
                .Bind<GameplayMenuEventHandler>()
                .FromNew()
                .AsSingle()
                .WithArguments(levelNumberTMP, keysNumberTMP);
        }
        #endregion
    }
}