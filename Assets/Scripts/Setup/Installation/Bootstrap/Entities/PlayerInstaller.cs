using Roguelike.Animation;
using Roguelike.Core.Gameplay.Events.Handling;
using Roguelike.Core.Gameplay.Events.Subscribing;
using Roguelike.Core.Gameplay.Interaction.Openers;
using Roguelike.Core.Gameplay.Interaction.Pickers;
using Roguelike.Core.Gameplay.Transformation.Moving;
using Roguelike.Core.Information;
using Roguelike.Gameplay.Interaction.Opening;
using Roguelike.Gameplay.Interaction.Picking;
using Roguelike.Utilities.Extensions.Extenject;
using Zenject;

namespace Roguelike.Setup.Installation.Bootstrap.Entities
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