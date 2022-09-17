using Roguelike.Core.Information;
using Roguelike.Core.Rotators;
using UnityEngine;

namespace Roguelike.Animators
{
    [RequireComponent(typeof(Animator)), RequireComponent(typeof(SpriteRotator))]
    public class PlayerAnimationChanger : MonoBehaviour
    {
        #region Fields
        [Header("Idle Animation Settings")]
        [SerializeField] float playerLeftRotatedIdleAnimationCycleOffset;
        [SerializeField] float playerRightRotatedIdleAnimationCycleOffset;
        [Header("Moving Animation Settings")]
        [SerializeField] float movingAnimationTime;
        
        Animator animator;
        SpriteRotator spriteRotator;
        #endregion
        
        #region Methods
        void Awake()
        {
            animator = GetComponent<Animator>();
            spriteRotator = GetComponent<SpriteRotator>();
        }
        
        public void ActivateDeathAnimation() => animator.SetBool(AnimatorParameter.IsDead.ToString(), true);
        public void ActivateMovingAnimation() => animator.SetBool(AnimatorParameter.IsMoving.ToString(), true);
        public void DeactivateMovingAnimation() => animator.SetBool(AnimatorParameter.IsMoving.ToString(), false);
        public void DeactivateMovingAnimationAfterItFinished() =>
            Invoke(nameof(DeactivateMovingAnimation), movingAnimationTime);
        
        public void SetIdleCycleOffset()
        {
            animator.SetFloat(AnimatorParameter.IdleCycleOffset.ToString(), spriteRotator.IsLeftRotated() 
                ? playerLeftRotatedIdleAnimationCycleOffset : playerRightRotatedIdleAnimationCycleOffset);
        }
        #endregion
    }
}