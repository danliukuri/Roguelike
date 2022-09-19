using Roguelike.Core.Information;
using Roguelike.Core.Rotators;
using Roguelike.Utilities.Extensions;
using UnityEngine;

namespace Roguelike.Core.EventHandlers
{
    public class TargetDetectionStatusEventHandler
    {
        #region Fields
        readonly Animator animator;
        readonly SpriteRotator rotator;
        #endregion
        
        #region Methods
        public TargetDetectionStatusEventHandler(Animator animator, SpriteRotator rotator)
        {
            this.animator = animator;
            this.rotator = rotator;
        }
        
        public void OnParentTargetDetected(object sender, MovingEventArgs e) =>
            animator.SetBool(AnimatorParameter.IsDetected, true);
        public void OnParentTargetOverlooked(object sender, MovingEventArgs e) =>
            animator.SetBool(AnimatorParameter.IsDetected, false);
        
        public void OnEnable(bool isParentLeftRotated)
        {
            if (isParentLeftRotated)
                OnParentRotatedLeft();
            else
                OnParentRotatedRight();
        }
        public void OnParentRotatedLeft() => rotator.RotateLeft();
        public void OnParentRotatedRight() => rotator.RotateRight();
        #endregion
    }
}