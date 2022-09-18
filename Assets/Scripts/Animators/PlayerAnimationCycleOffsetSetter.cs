using Roguelike.Core.Information;
using Roguelike.Core.Rotators;
using Roguelike.Utilities.Extensions;
using UnityEngine;

namespace Roguelike.Animators
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