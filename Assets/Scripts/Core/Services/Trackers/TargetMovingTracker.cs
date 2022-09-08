using System;
using Roguelike.Core.Information;
using UnityEngine;

namespace Roguelike.Core.Services.Trackers
{
    public class TargetMovingTracker : IResettable
    {
        #region Properties
        public MovingEventArgs TargetMovingEventArgs { get; set; }
        public bool IsTargetDetected
        {
            get => isTargetDetected;
            set
            {
                isTargetDetected = value;
                if (isTargetDetected)
                    TargetDetected?.Invoke(this, TargetMovingEventArgs);
                else
                    TargetOverlooked?.Invoke(this, TargetMovingEventArgs);
            }
        }
        #endregion
        
        #region Fields
        public event EventHandler<MovingEventArgs> TargetDetected;
        public event EventHandler<MovingEventArgs> TargetOverlooked;

        bool isTargetDetected;
        #endregion

        #region Methods
        public void Reset() => TargetMovingEventArgs = default;
        
        public bool IsTargetOnPosition(Vector3 position) =>
            TargetMovingEventArgs == default || TargetMovingEventArgs.Destination == position;
        #endregion
    }
}