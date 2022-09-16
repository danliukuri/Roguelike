using Roguelike.Animators;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Roguelike.UI.EventHandlers.Buttons
{
    public class ButtonWithPlayerTilesEventHandler : MonoBehaviour,
        IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        #region Fields
        [SerializeField] PlayerTileAnimationChanger[] playerAnimationChangers;
        #endregion
        
        #region Methods
        public void OnPointerEnter(PointerEventData eventData)
        {
            foreach (PlayerTileAnimationChanger playerAnimationChanger in playerAnimationChangers)
                OnPointerEnter(playerAnimationChanger);
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            foreach (PlayerTileAnimationChanger playerAnimationChanger in playerAnimationChangers)
                OnPointerExit(playerAnimationChanger);
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            foreach (PlayerTileAnimationChanger playerAnimationChanger in playerAnimationChangers)
                OnPointerClick(playerAnimationChanger);
        }
        
        protected virtual void OnPointerEnter(PlayerTileAnimationChanger playerAnimationChanger)
        {
            if (playerAnimationChanger.IsInvoking(nameof(playerAnimationChanger.DisableGameObject)))
            {
                playerAnimationChanger.CancelInvoke(nameof(playerAnimationChanger.DisableGameObject));
                playerAnimationChanger.DeactivateDespawningAnimation();
            }
            else
                playerAnimationChanger.gameObject.SetActive(true);
            
            playerAnimationChanger.ActivateSpawningAnimation();
        }
        protected virtual void OnPointerExit(PlayerTileAnimationChanger playerAnimationChanger)
        {
            playerAnimationChanger.ActivateDespawningAnimation();
            playerAnimationChanger.InvokeAfterAnimationFinished(playerAnimationChanger.DisableGameObject);
        }
        protected virtual void OnPointerClick(PlayerTileAnimationChanger playerAnimationChanger) =>
            playerAnimationChanger.gameObject.SetActive(false);
        #endregion
    }
}