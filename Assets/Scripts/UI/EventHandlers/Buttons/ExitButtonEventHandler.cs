using Roguelike.Animators;

namespace Roguelike.UI.EventHandlers.Buttons
{
    public class ExitButtonEventHandler : ButtonWithPlayerTilesEventHandler
    {
        #region Methods
        protected override void OnPointerEnter(PlayerTileAnimationChanger playerAnimationChanger)
        {
            base.OnPointerEnter(playerAnimationChanger);
            playerAnimationChanger.InvokeAfterAnimationFinished(playerAnimationChanger.ActivateNoHeadShakingAnimation);
        }
        protected override void OnPointerExit(PlayerTileAnimationChanger playerAnimationChanger)
        {
            playerAnimationChanger.DeactivateNoHeadShakingAnimation();
            base.OnPointerExit(playerAnimationChanger);
        }
        #endregion
    }
}