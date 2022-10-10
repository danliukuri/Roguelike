using System;
using Roguelike.Core.Gameplay.Services.Input;
using Roguelike.Core.Information;
using UnityEngine;
using Zenject;

namespace Roguelike.Gameplay.Services.Input
{
    public class PhoneUserMovementInput : IMovementInputService, ITickable
    {
        #region Events
        public event EventHandler<MovingEventArgs> Moving;
        #endregion
        
        #region Fields
        float timeToHoldMoveKeyBeforeMoving;
        float moveKeyHoldTime;
        (int CurrentWidth, int CurrentHeight, float AspectRatio) screen;
        #endregion
        
        #region Methods
        [Inject]
        void Construct(float timeToHoldMoveKeyBeforeMoving) =>
            this.timeToHoldMoveKeyBeforeMoving = timeToHoldMoveKeyBeforeMoving;
        
        void SetScreenParametersIfNew()
        {
            if (screen.CurrentWidth != Screen.width || screen.CurrentHeight != Screen.height)
                screen = (Screen.width, Screen.height, (float)Screen.width / Screen.height);
        }
        
        public void Tick() => TrackMovementEvents();
        void TrackMovementEvents()
        {
            moveKeyHoldTime += Time.deltaTime;
            if (UnityEngine.Input.touchCount > 0 && moveKeyHoldTime >= timeToHoldMoveKeyBeforeMoving)
            {
                SetScreenParametersIfNew();
                Touch touch = UnityEngine.Input.GetTouch(default);
                
                if (touch.position.x > touch.position.y * screen.AspectRatio)
                {
                    if (screen.CurrentWidth - touch.position.x < touch.position.y * screen.AspectRatio)
                        InvokeMovementEvent(Vector3.right);
                    else if (screen.CurrentWidth - touch.position.x > touch.position.y * screen.AspectRatio)
                        InvokeMovementEvent(Vector3.down);
                }
                else if(touch.position.x < touch.position.y * screen.AspectRatio)
                {
                    if (screen.CurrentWidth - touch.position.x < touch.position.y * screen.AspectRatio)
                        InvokeMovementEvent(Vector3.up);
                    else if (screen.CurrentWidth - touch.position.x > touch.position.y * screen.AspectRatio)
                        InvokeMovementEvent(Vector3.left);
                }
            }
        }
        
        void InvokeMovementEvent(Vector3 direction)
        {
            Moving?.Invoke(this, new MovingEventArgs { Destination = direction });
            moveKeyHoldTime = default;
        }
        #endregion
    }
}