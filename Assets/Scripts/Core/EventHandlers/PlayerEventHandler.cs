using Roguelike.Animators;
using Roguelike.Core.EventSubscribers;
using Roguelike.Core.Information;
using Roguelike.Core.Movers;
using Roguelike.Core.Openers;
using Roguelike.Core.Pickers;

namespace Roguelike.Core.EventHandlers
{
    public class PlayerEventHandler
    {
        #region Fields
        readonly EntityMover mover;
        readonly IPicker keyPicker;
        readonly IOpener doorOpener;
        readonly PlayerAnimationChanger animationChanger;
        PlayerEventSubscriber subscriber;
        #endregion
        
        #region Methods
        public PlayerEventHandler(EntityMover mover, IPicker keyPicker, IOpener doorOpener,
            PlayerAnimationChanger animationChanger)
        {
            this.mover = mover;
            this.keyPicker = keyPicker;
            this.doorOpener = doorOpener;
            this.animationChanger = animationChanger;
        }
        public void SetSubscriber(PlayerEventSubscriber value) => subscriber = value;
        
        public void OnInputServiceMoving(object sender, MovingEventArgs e) => mover.TryToMove(e.Destination);
        public void OnMoving(object sender, MovingEventArgs e)
        {
            animationChanger.ActivateMovingAnimation();
            animationChanger.DeactivateMovingAnimationAfterItFinished();
            animationChanger.SetIdleCycleOffset();
        }
        public void OnMovingToKey(object sender, MovingEventArgs e) => keyPicker.TryToPickUp(e.Element);
        public void OnMovingToDoor(object sender, MovingEventArgs e) => doorOpener.TryToOpen(e.Element);
        public void OnPlayerDeath(object sender, MovingEventArgs e)
        {
            subscriber.UnsubscribeFromInputServiceMovingEvent();
            animationChanger.ActivateDeathAnimation();
        }
        #endregion
    }
}