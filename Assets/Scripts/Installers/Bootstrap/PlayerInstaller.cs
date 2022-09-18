using Roguelike.Animators;
using Roguelike.Core.EventHandlers;
using Roguelike.Core.EventSubscribers;
using Roguelike.Core.Information;
using Roguelike.Core.Movers;
using Roguelike.Core.Openers;
using Roguelike.Core.Pickers;
using Roguelike.Openers;
using Roguelike.Pickers;
using Roguelike.Utilities.Extensions.Extenject;
using Zenject;

namespace Roguelike.Installers.Bootstrap
{
    public class PlayerInstaller : MonoInstaller
    {
        #region Methods
        public override void InstallBindings()
        {
            BindMover();
            BindInventory();
            BindKeyPicker();
            BindDoorOpener();
            BindEventHandler();
            BindAnimationActivator();
            BindIdleAnimationCycleOffsetSetter();
        }
        void BindMover()
        {
            Container
                .Bind<EntityMover>()
                .FromComponentSibling()
                .AsSingle();
        }
        void BindInventory()
        {
            Container
                .Bind<Inventory>()
                .AsSingle();
        }
        void BindKeyPicker()
        {
            Container
                .Bind<IPicker>()
                .To<KeyPicker>()
                .AsTransient()
                .WhenInjectedInto<PlayerEventHandler>();
        }        
        void BindDoorOpener()
        {
            Container
                .Bind<IOpener>()
                .To<DoorOpener>()
                .AsTransient()
                .WhenInjectedInto<PlayerEventHandler>();
        }
        void BindEventHandler()
        {
            Container
                .Bind<PlayerEventHandler>()
                .AsSingle()
                .WhenInjectedInto<PlayerEventSubscriber>();
        }
        void BindAnimationActivator()
        {
            Container
                .Bind<PlayerAnimationActivator>()
                .FromComponentOnParentContextGameObject()
                .AsTransient()
                .WhenInjectedInto<PlayerEventHandler>();
        }
        void BindIdleAnimationCycleOffsetSetter()
        {
            Container
                .Bind<PlayerAnimationCycleOffsetSetter>()
                .FromComponentOnParentContextGameObject()
                .AsTransient()
                .WhenInjectedInto<PlayerEventHandler>();
        }
        #endregion
    }
}