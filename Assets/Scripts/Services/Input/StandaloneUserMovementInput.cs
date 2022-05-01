using Roguelike.Core.Services.Input;
using System;
using UnityEngine;
using Zenject;

namespace Roguelike.Services.Input
{
    public class StandaloneUserMovementInput : IMovementInputService, ITickable
    {
        #region Fields
        public event Action<Vector3> Moving;

        const string horizontalAxisName = "Horizontal";
        const string verticalAxisName = "Vertical";
        const int defaultAxisValue = 0;

        float timeToHoldMoveKeyBeforeMoving;
        float timeHeldMoveKey;
        #endregion

        #region Methods
        [Inject]
        public void Construct(float timeToHoldMoveKeyBeforeMoving)
        {
            this.timeToHoldMoveKeyBeforeMoving = timeToHoldMoveKeyBeforeMoving;
        }

        public void Tick()
        {
            TrackMovementEvents();
        }

        void TrackMovementEvents()
        {
            timeHeldMoveKey += Time.deltaTime;
            if (timeHeldMoveKey > timeToHoldMoveKeyBeforeMoving)
            {
                bool isHorizontalAxisButtonHeldDown = UnityEngine.Input.GetButton(horizontalAxisName);
                bool isVerticalAxisButtonHeldDown = UnityEngine.Input.GetButton(verticalAxisName);

                if (isHorizontalAxisButtonHeldDown && !isVerticalAxisButtonHeldDown)
                {
                    timeHeldMoveKey = 0f;
                    InvokeMovementEvent(UnityEngine.Input.GetAxis(horizontalAxisName), Vector3.right, Vector3.left);
                }
                else if (isVerticalAxisButtonHeldDown && !isHorizontalAxisButtonHeldDown)
                {
                    timeHeldMoveKey = 0f;
                    InvokeMovementEvent(UnityEngine.Input.GetAxis(verticalAxisName), Vector3.up, Vector3.down);
                }
            }
        }
        void InvokeMovementEvent(float axisValue, Vector3 positiveDirection, Vector3 negativeDirection)
        {
            if (axisValue > defaultAxisValue)
                Moving?.Invoke(positiveDirection);
            else if (axisValue < defaultAxisValue)
                Moving?.Invoke(negativeDirection);
        }
        #endregion
    }
}