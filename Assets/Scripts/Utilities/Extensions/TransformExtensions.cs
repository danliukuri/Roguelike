using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Roguelike.Utilities.Extensions
{
    public static class TransformExtensions
    {
        #region Methods
        public static void RotateRandomNumberOfTimesByRightAngle(this Transform source, Vector3 axis,
            Action onRotateAction = default)
        {
            source.CheckForNullException(nameof(source));
            axis.CheckForNullException(nameof(axis));
            
            const int MinNumberOfTurns = 0, MaxNumberOfTurns = 3;
            int randomNumberOfTurns = Random.Range(MinNumberOfTurns, MaxNumberOfTurns + 1);
            for (int i = 0; i < randomNumberOfTurns; i++)
                source.RotateRightAngle(axis, onRotateAction, true);
        }
        public static void RotateRightAngle(this Transform source, Vector3 axis, Action onRotateAction = default,
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
        }
        #endregion
    }
}