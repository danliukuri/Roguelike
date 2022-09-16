using Roguelike.Animators;

namespace Roguelike.UI.EventHandlers.Buttons
{
    public class PlayButtonEventHandler : ButtonWithPlayerTilesEventHandler
    {
        #region Methods
        protected override void OnPointerEnter(PlayerTileAnimationChanger playerAnimationChanger)
        {
            base.OnPointerEnter(playerAnimationChanger);
            playerAnimationChanger.InvokeAfterAnimationFinished(playerAnimationChanger.ActivateMovingAnimation);
        }
        protected override void OnPointerExit(PlayerTileAnimationChanger playerAnimationChanger)
        {
            playerAnimationChanger.DeactivateMovingAnimation();
            base.OnPointerExit(playerAnimationChanger);
        }
        #endregion
    }
}