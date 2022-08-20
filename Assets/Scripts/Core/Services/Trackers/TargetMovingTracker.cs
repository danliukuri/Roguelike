using Roguelike.Core.Information;
using UnityEngine;

namespace Roguelike.Core.Services.Trackers
{
    public class TargetMovingTracker : IResettable
    {
        #region Properties
        public MovingEventArgs TargetMovingEventArgs { get; set; }
        #endregion
        
        #region Methods
        public void Reset() => TargetMovingEventArgs = default;
        
        public bool IsTargetOnPosition(Vector3 position) =>
            TargetMovingEventArgs == default || TargetMovingEventArgs.Destination == position;
        #endregion
    }
}