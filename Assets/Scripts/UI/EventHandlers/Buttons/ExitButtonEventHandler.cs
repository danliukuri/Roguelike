using Roguelike.Animators;

namespace Roguelike.UI.EventHandlers.Buttons
{
    public class ExitButtonEventHandler : ButtonWithPlayerTilesEventHandler
    {
        #region Methods
        protected override void OnPointerEnter(PlayerAnimationActivator playerAnimationActivator)
        {
            base.OnPointerEnter(playerAnimationActivator);
            playerAnimationActivator.StartInvokeCoroutineAfterCurrentAnimationFinished(
                playerAnimationActivator.ActivateNoHeadShakingAnimation);
        }
        protected override void OnPointerExit(PlayerAnimationActivator playerAnimationActivator)
        {
            playerAnimationActivator.DeactivateNoHeadShakingAnimation();
            base.OnPointerExit(playerAnimationActivator);
        }
        #endregion
    }
}