using UnityEngine;

namespace Roguelike.Utilities.Extensions
{
    public static class TransformExtensions
    {
        #region Methods
        public static void RotateRandomNumberOfTimesByRightAngle(this Transform sourse,
            Vector3 axis, System.Action onRotateAction = null)
        {
            sourse.CheckForNullException(nameof(sourse));
            axis.CheckForNullException(nameof(axis));

            const float RightAngle = 90f;
            const int MinNumberOfTurns = 0, MaxNumberOfTurns = 3;

            int randomNumberOfTurns = Random.Range(MinNumberOfTurns, MaxNumberOfTurns + 1);
            for (int i = 0; i < randomNumberOfTurns; i++)
            {
                sourse.Rotate(axis, RightAngle);
                onRotateAction?.Invoke();
            }
        }
        #endregion
    }
}