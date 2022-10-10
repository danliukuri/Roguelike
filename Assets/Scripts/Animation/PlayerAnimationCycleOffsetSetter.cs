using Roguelike.Animation.Information;
using Roguelike.Core.Gameplay.Transformation.Rotation;
using Roguelike.Utilities.Extensions.Unity.Animation;
using UnityEngine;

namespace Roguelike.Animation
{
    [RequireComponent(typeof(Animator)), RequireComponent(typeof(SpriteRotator))]
    public class PlayerAnimationCycleOffsetSetter : MonoBehaviour
    {
        #region Fields
        [SerializeField] AnimatorParameter cycleOffset;
        [SerializeField] float playerLeftRotatedIdleAnimationCycleOffset;
        [SerializeField] float playerRightRotatedIdleAnimationCycleOffset;
        
        Animator animator;
        SpriteRotator spriteRotator;
        #endregion
        
        #region Methods
        void Awake()
        {
            animator = GetComponent<Animator>();
            spriteRotator = GetComponent<SpriteRotator>();
        }
        public void Set() => animator.SetFloat(cycleOffset, spriteRotator.IsLeftRotated() 
            ? playerLeftRotatedIdleAnimationCycleOffset : playerRightRotatedIdleAnimationCycleOffset);
        #endregion
    }
}