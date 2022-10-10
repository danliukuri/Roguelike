using Roguelike.Animation;
using Roguelike.Core.Gameplay.Events.Subscribing;
using Roguelike.Core.Gameplay.Interaction.Openers;
using Roguelike.Core.Gameplay.Interaction.Pickers;
using Roguelike.Core.Gameplay.Transformation.Moving;
using Roguelike.Core.Information;

namespace Roguelike.Core.Gameplay.Events.Handling
{
    public class PlayerEventHandler
    {
        #region Fields
        readonly EntityMover mover;
        readonly IPicker keyPicker;
        readonly IOpener doorOpener;
        readonly PlayerAnimationActivator animationActivator;
        readonly PlayerAnimationCycleOffsetSetter idleAnimationCycleOffsetSetter;
        PlayerEventSubscriber subscriber;
        #endregion
        
        #region Methods
        public PlayerEventHandler(EntityMover mover, IPicker keyPicker, IOpener doorOpener,
            PlayerAnimationActivator animationActivator,
            PlayerAnimationCycleOffsetSetter idleAnimationCycleOffsetSetter)
        {
            this.mover = mover;
            this.keyPicker = keyPicker;
            this.doorOpener = doorOpener;
            this.animationActivator = animationActivator;
            this.idleAnimationCycleOffsetSetter = idleAnimationCycleOffsetSetter;
        }
        public void SetSubscriber(PlayerEventSubscriber value) => subscriber = value;
        
        public void OnInputServiceMoving(object sender, MovingEventArgs e) => mover.TryToMove(e.Destination);
        public void OnMoving(object sender, MovingEventArgs e)
        {
            animationActivator.ActivateMovingAnimation();
            animationActivator.StartInvokeCoroutineAfterCurrentAnimationFinished(
                animationActivator.DeactivateMovingAnimation);
            idleAnimationCycleOffsetSetter.Set();
        }
        public void OnMovingToKey(object sender, MovingEventArgs e) => keyPicker.TryToPickUp(e.Element);
        public void OnMovingToDoor(object sender, MovingEventArgs e) => doorOpener.TryToOpen(e.Element);
        public void OnPlayerDeath(object sender, MovingEventArgs e)
        {
            subscriber.UnsubscribeFromInputServiceMovingEvent();
            animationActivator.ActivateDeathAnimation();
        }
        #endregion
    }
}