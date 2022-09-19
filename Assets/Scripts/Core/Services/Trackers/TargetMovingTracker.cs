using System;
using Roguelike.Core.Information;
using UnityEngine;

namespace Roguelike.Core.Services.Trackers
{
    public class TargetMovingTracker : IResettable
    {
        #region Properties
        public MovingEventArgs TargetMovingEventArgs { get; private set; }
        public bool IsTargetDetected
        {
            get => isTargetDetected;
            private set
            {
                if (isTargetDetected != value)
                {
                    isTargetDetected = value;
                    if (isTargetDetected)
                        TargetDetected?.Invoke(this, TargetMovingEventArgs);
                    else
                    {
                        StopOverlookingTarget();
                        TargetOverlooked?.Invoke(this, TargetMovingEventArgs);
                    }
                }
            }
        }
        #endregion
        
        #region Fields
        public event EventHandler<MovingEventArgs> TargetDetected;
        public event EventHandler<MovingEventArgs> TargetOverlooked;
        
        readonly int requiredNumberOfTriesToOverlookTarget;
        int currentNumberOfTriesToOverlookTarget;
        bool isTargetDetected;
        #endregion
        
        #region Methods
        public TargetMovingTracker(int requiredNumberOfTriesToOverlookTarget) =>
            this.requiredNumberOfTriesToOverlookTarget = requiredNumberOfTriesToOverlookTarget;
        public void Reset() => OverlookTarget();
        
        public bool IsTargetOnPosition(Vector3 position) =>
            TargetMovingEventArgs == default || TargetMovingEventArgs.Destination == position;
        
        public void DetectTarget(MovingEventArgs targetMovingEventArgs)
        {
            TargetMovingEventArgs = targetMovingEventArgs;
            IsTargetDetected = true;
        }
        public void OverlookTarget()
        {
            TargetMovingEventArgs = default;
            IsTargetDetected = default;
        }
        
        public bool TryToOverlookTarget()
        {
            bool isNeededToOverlookTarget =
                ++currentNumberOfTriesToOverlookTarget >= requiredNumberOfTriesToOverlookTarget;
            if (isNeededToOverlookTarget)
                OverlookTarget();
            return isNeededToOverlookTarget;
        }
        public void StopOverlookingTarget() => currentNumberOfTriesToOverlookTarget = default;
        #endregion
    }
}