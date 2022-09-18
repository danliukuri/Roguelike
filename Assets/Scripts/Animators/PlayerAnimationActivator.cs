using System;
using Roguelike.Core.Information;
using Roguelike.Utilities.Extensions;
using UnityEngine;

namespace Roguelike.Animators
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimationActivator : MonoBehaviour
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
        public void ActivateDeathAnimation() => animator.SetBool(AnimatorParameter.IsDead, true);
        public void DeactivateDeathAnimation() => animator.SetBool(AnimatorParameter.IsDead, false);

        public void DeactivateGameObject() => gameObject.SetActive(false);
        public void StartInvokeCoroutineAfterCurrentAnimationFinished(Action action) =>
            this.StartInvokeCoroutineAfterCurrentAnimationFinished(action, animator);
        #endregion
    }
}