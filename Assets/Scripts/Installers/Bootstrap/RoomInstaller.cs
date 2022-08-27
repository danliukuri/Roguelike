using Roguelike.Core.EventHandlers;
using Roguelike.Core.EventSubscribers;
using Roguelike.Core.Services.Controllers;
using Zenject;

namespace Roguelike.Installers.Bootstrap
{
    public class RoomInstaller : MonoInstaller
    {
        #region Methods
        public override void InstallBindings()
        {
            BackgroundController();
            BindEventHandler();
        }
        void BackgroundController()
        {
            Container
                .Bind<RoomBackgroundController>()
                .FromComponentSibling()
                .AsTransient()
                .WhenInjectedInto<RoomEventSubscriber>();
        }
        void BindEventHandler()
        {
            Container
                .Bind<RoomEventHandler>()
                .FromNew()
                .AsTransient()
                .WhenInjectedInto<RoomEventSubscriber>();
        }
        #endregion
    }
}