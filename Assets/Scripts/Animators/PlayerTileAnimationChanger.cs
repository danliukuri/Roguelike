using System;
using Roguelike.Core.Information;
using Roguelike.Utilities.Extensions;
using UnityEngine;

namespace Roguelike.Animators
{
    [RequireComponent(typeof(Animator))]
    public class PlayerTileAnimationChanger : MonoBehaviour
    {
        #region Fields
        Animator animator;
        #endregion
        
        #region Methods
        void Awake() => animator = GetComponent<Animator>();
        
        public void ActivateSpawningAnimation() => animator.SetTrigger(AnimatorParameter.SpawnTrigger);
        public void ActivateMovingAnimation() => animator.SetBool(AnimatorParameter.IsMoving, true);
        public void DeactivateMovingAnimation() => animator.SetBool(AnimatorParameter.IsMoving, false);
        public void ActivateNoHeadShakingAnimation() => animator.SetBool(AnimatorParameter.IsNoHeadShaking, true);
        public void DeactivateNoHeadShakingAnimation() => animator.SetBool(AnimatorParameter.IsNoHeadShaking, false);
        public void ActivateDespawningAnimation() => animator.SetBool(AnimatorParameter.IsDespawning, true);
        public void DeactivateDespawningAnimation() => animator.SetBool(AnimatorParameter.IsDespawning, false);
        
        public void DisableGameObject() => gameObject.SetActive(false);
        public void InvokeAfterAnimationFinished(Action playerTileAnimationChangerAction, int layerIndex = default) =>
            Invoke(playerTileAnimationChangerAction.Method.Name, GetCurrentAnimatorStateLength(layerIndex));
        float GetCurrentAnimatorStateLength(int layerIndex = default)
        {
            animator.Update(default);
            return animator.GetCurrentAnimatorStateInfo(layerIndex).length;
        }
        #endregion
    }
}