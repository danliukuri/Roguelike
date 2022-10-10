using Roguelike.Core.Gameplay.Events.Handling;
using Roguelike.Core.Gameplay.Events.Subscribing;
using Roguelike.Core.Gameplay.Transformation.Activation;
using Zenject;

namespace Roguelike.Setup.Installation.Bootstrap.Entities
{
    public class RoomInstaller : MonoInstaller
    {
        #region Methods
        public override void InstallBindings()
        {
            BindPassagesBackgroundActivator();
            BindEventHandler();
        }
        void BindPassagesBackgroundActivator()
        {
            Container
                .Bind<RoomPassagesBackgroundActivator>()
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