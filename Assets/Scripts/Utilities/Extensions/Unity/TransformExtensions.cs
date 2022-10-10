using System;
using Roguelike.Utilities.Extensions.Generic;
using UnityEngine;

namespace Roguelike.Utilities.Extensions.Unity
{
    public static class TransformExtensions
    {
        #region Methods
        public static Transform RotateRandomNumberOfTimesByRightAngle(this Transform source, Vector3 axis,
            Action onRotateAction = default)
        {
            source.CheckForNullException(nameof(source));
            axis.CheckForNullException(nameof(axis));
            
            const int MinNumberOfTurns = 0, MaxNumberOfTurns = 3;
            RandomExtensions.InvokeRandomNumberOfTimes(() => source.RotateRightAngle(axis, onRotateAction, true),
                MinNumberOfTurns, MaxNumberOfTurns);
            return source;
        }
        public static Transform RotateRightAngle(this Transform source, Vector3 axis, Action onRotateAction = default,
            bool isParametersChecked = false)
        {
            if (!isParametersChecked)
            {
                source.CheckForNullException(nameof(source));
                axis.CheckForNullException(nameof(axis)); 
            }
            
            const float RightAngle = 90f;
            source.Rotate(axis, RightAngle);
            onRotateAction?.Invoke();
            return source;
        }
        #endregion
    }
}