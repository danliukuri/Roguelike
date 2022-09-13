using Roguelike.Core.Rotators;
using UnityEngine;

namespace Roguelike.Animators
{
    [RequireComponent(typeof(SpriteRotator))]
    public class PlayerAnimationChanger : MonoBehaviour
    {
        #region Fields
        [Header("Idle Animation Settings")]
        [SerializeField] AnimatorParameter idleCycleOffset;
        [SerializeField] float playerLeftRotatedIdleAnimationCycleOffset;
        [SerializeField] float playerRightRotatedIdleAnimationCycleOffset;
        [Header("Moving Animation Settings")]
        [SerializeField] AnimatorParameter isMoving;
        [SerializeField] float movingAnimationTime;
        
        SpriteRotator spriteRotator;
        #endregion
        
        #region Methods
        void Awake() => spriteRotator = GetComponent<SpriteRotator>();
        
        public void ActivateMovingAnimation() => isMoving.Set(true);
        public void DeactivateMovingAnimationAfterItFinished() =>
            isMoving.Invoke(nameof(isMoving.SetDefaultBool), movingAnimationTime);
        
        public void SetIdleCycleOffset()
        {
            idleCycleOffset.Set(spriteRotator.IsLeftRotated()
                ? playerLeftRotatedIdleAnimationCycleOffset
                : playerRightRotatedIdleAnimationCycleOffset);
        }
        #endregion
    }
}