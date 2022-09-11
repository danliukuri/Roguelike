using System;
using Roguelike.Core.Rotators;
using Random = UnityEngine.Random;

namespace Roguelike.Utilities.Extensions
{
    public static class RandomExtensions
    {
        #region Fields
        const float equalProbability = 0.5f;
        #endregion
        
        #region Methods
        public static bool BoolValue(float trueValueProbability = equalProbability) =>
            Random.value < trueValueProbability;
        public static void InvokeRandom(Action firstAction, Action secondAction,
            float firstActionInvocationProbability = equalProbability)
        {
            if(BoolValue(firstActionInvocationProbability)) 
                firstAction?.Invoke();
            else 
                secondAction?.Invoke();
        }
        
        public static SpriteRotator RandomRotateRightOrLeft(this SpriteRotator rotator,
            float leftRotationProbability = equalProbability)
        {
            InvokeRandom(rotator.RotateLeft, rotator.RotateRight, leftRotationProbability);
            return rotator;
        }
        public static void InvokeRandomNumberOfTimes(this Action action, int minNumberOfInvokes, int maxNumberOfInvokes)
        {
            action.CheckForNullException(nameof(action));
            int randomNumberOfInvokes = Random.Range(minNumberOfInvokes, maxNumberOfInvokes + 1);
            for (int i = 0; i < randomNumberOfInvokes; i++)
                action.Invoke();
        }
        #endregion
    }
}