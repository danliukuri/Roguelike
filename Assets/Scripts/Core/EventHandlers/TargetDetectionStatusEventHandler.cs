using Roguelike.Animators;
using Roguelike.Core.Information;
using Roguelike.Core.Rotators;

namespace Roguelike.Core.EventHandlers
{
    public class TargetDetectionStatusEventHandler
    {
        #region Fields
        AnimatorParameter isDetected;
        SpriteRotator rotator;
        #endregion
        
        #region Methods
        public TargetDetectionStatusEventHandler(AnimatorParameter isDetected, SpriteRotator rotator)
        {
            this.isDetected = isDetected;
            this.rotator = rotator;
        }
        
        public void OnParentTargetDetected(object sender, MovingEventArgs e) => isDetected.Set(true);
        public void OnParentTargetOverlooked(object sender, MovingEventArgs e) => isDetected.Set(false);
        
        public void OnParentRotatedLeft() => rotator.RotateLeft();
        public void OnParentRotatedRight() => rotator.RotateRight();
        #endregion
    }
}