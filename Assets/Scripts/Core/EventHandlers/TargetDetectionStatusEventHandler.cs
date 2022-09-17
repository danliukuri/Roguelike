using Roguelike.Core.Information;
using Roguelike.Core.Rotators;
using UnityEngine;

namespace Roguelike.Core.EventHandlers
{
    public class TargetDetectionStatusEventHandler
    {
        #region Fields
        Animator animator;
        SpriteRotator rotator;
        #endregion
        
        #region Methods
        public TargetDetectionStatusEventHandler(Animator animator, SpriteRotator rotator)
        {
            this.animator = animator;
            this.rotator = rotator;
        }
        
        public void OnParentTargetDetected(object sender, MovingEventArgs e) =>
            animator.SetBool(AnimatorParameter.IsDetected.ToString(), true);
        public void OnParentTargetOverlooked(object sender, MovingEventArgs e) =>
            animator.SetBool(AnimatorParameter.IsDetected.ToString(), false);
        
        public void OnParentRotatedLeft() => rotator.RotateLeft();
        public void OnParentRotatedRight() => rotator.RotateRight();
        #endregion
    }
}