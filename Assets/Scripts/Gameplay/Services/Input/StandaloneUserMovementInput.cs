using System;
using Roguelike.Core.Gameplay.Services.Input;
using Roguelike.Core.Information;
using UnityEngine;
using Zenject;

namespace Roguelike.Gameplay.Services.Input
{
    public class StandaloneUserMovementInput : IMovementInputService, ITickable
    {
        #region Events
        public event EventHandler<MovingEventArgs> Moving;
        #endregion
        
        #region Fields
        const string HorizontalAxisName = "Horizontal",  VerticalAxisName = "Vertical";
        const int DefaultAxisValue = default;
        
        float timeToHoldMoveKeyBeforeMoving;
        float moveKeyHoldTime;
        #endregion
        
        #region Methods
        [Inject]
        public void Construct(float timeToHoldMoveKeyBeforeMoving) =>
            this.timeToHoldMoveKeyBeforeMoving = timeToHoldMoveKeyBeforeMoving;
        
        public void Tick() => TrackMovementEvents();
        void TrackMovementEvents()
        {
            moveKeyHoldTime += Time.deltaTime;
            if (moveKeyHoldTime > timeToHoldMoveKeyBeforeMoving)
            {
                bool isHorizontalAxisButtonHeldDown = UnityEngine.Input.GetButton(HorizontalAxisName);
                bool isVerticalAxisButtonHeldDown = UnityEngine.Input.GetButton(VerticalAxisName);
                
                if (isHorizontalAxisButtonHeldDown && !isVerticalAxisButtonHeldDown)
                    InvokeMovementEvent(UnityEngine.Input.GetAxis(HorizontalAxisName), Vector3.right, Vector3.left);
                else if (isVerticalAxisButtonHeldDown && !isHorizontalAxisButtonHeldDown)
                    InvokeMovementEvent(UnityEngine.Input.GetAxis(VerticalAxisName), Vector3.up, Vector3.down);
            }
        }
        void InvokeMovementEvent(float axisValue, Vector3 positiveDirection, Vector3 negativeDirection)
        {
            if (axisValue > DefaultAxisValue)
                Moving?.Invoke(this, new MovingEventArgs { Destination = positiveDirection });
            else if (axisValue < DefaultAxisValue)
                Moving?.Invoke(this, new MovingEventArgs { Destination = negativeDirection });
            
            moveKeyHoldTime = default;
        }
        #endregion
    }
}