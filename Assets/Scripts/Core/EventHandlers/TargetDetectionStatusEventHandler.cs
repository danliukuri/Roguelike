using Roguelike.Animators;
using Roguelike.Core.Information;

namespace Roguelike.Core.EventHandlers
{
    public class TargetDetectionStatusEventHandler
    {
        #region Fields
        AnimatorParameter isDetected;
        #endregion
        
        #region Methods
        public TargetDetectionStatusEventHandler(AnimatorParameter isDetected) => this.isDetected = isDetected;
        
        public void OnParentTargetDetected(object sender, MovingEventArgs e) => isDetected.Set(true);
        public void OnParentTargetOverlooked(object sender, MovingEventArgs e) => isDetected.Set(false);
        #endregion
    }
}