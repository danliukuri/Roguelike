using Roguelike.Animators;

namespace Roguelike.UI.EventHandlers.Buttons
{
    public class PlayButtonEventHandler : ButtonWithPlayerTilesEventHandler
    {
        #region Methods
        protected override void OnPointerEnter(PlayerAnimationActivator playerAnimationActivator)
        {
            base.OnPointerEnter(playerAnimationActivator);
            playerAnimationActivator.StartInvokeCoroutineAfterCurrentAnimationFinished(
                playerAnimationActivator.ActivateMovingAnimation);
        }
        protected override void OnPointerExit(PlayerAnimationActivator playerAnimationActivator)
        {
            playerAnimationActivator.DeactivateMovingAnimation();
            base.OnPointerExit(playerAnimationActivator);
        }
        #endregion
    }
}