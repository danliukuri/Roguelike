using Roguelike.Animators;
using Roguelike.Utilities.Extensions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Roguelike.UI.EventHandlers.Buttons
{
    public class ButtonWithPlayerTilesEventHandler : MonoBehaviour,
        IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        #region Fields
        [SerializeField] PlayerAnimationActivator[] playerAnimationChangers;
        #endregion
        
        #region Methods
        public void OnPointerEnter(PointerEventData eventData)
        {
            foreach (PlayerAnimationActivator playerAnimationChanger in playerAnimationChangers)
                OnPointerEnter(playerAnimationChanger);
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            foreach (PlayerAnimationActivator playerAnimationChanger in playerAnimationChangers)
                if (playerAnimationChanger.gameObject.activeSelf)
                    OnPointerExit(playerAnimationChanger);
        }
        public void OnPointerClick(PointerEventData eventData)
        {
            foreach (PlayerAnimationActivator playerAnimationChanger in playerAnimationChangers)
                OnPointerClick(playerAnimationChanger);
        }
        
        protected virtual void OnPointerEnter(PlayerAnimationActivator playerAnimationActivator)
        {
            if (playerAnimationActivator.IsInvokeCoroutineRunning(playerAnimationActivator.DeactivateGameObject))
            {
                playerAnimationActivator.StopInvokeCoroutine(playerAnimationActivator.DeactivateGameObject);
                playerAnimationActivator.DeactivateDespawningAnimation();
            }
            else
                playerAnimationActivator.gameObject.SetActive(true);
            
            playerAnimationActivator.ActivateSpawningAnimation();
        }
        protected virtual void OnPointerExit(PlayerAnimationActivator playerAnimationActivator)
        {
            playerAnimationActivator.ActivateDespawningAnimation();
            playerAnimationActivator.StartInvokeCoroutineAfterCurrentAnimationFinished(
                playerAnimationActivator.DeactivateGameObject);
        }
        protected virtual void OnPointerClick(PlayerAnimationActivator playerAnimationActivator) =>
            playerAnimationActivator.gameObject.SetActive(false);
        #endregion
    }
}