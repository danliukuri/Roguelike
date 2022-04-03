using System;
using System.Collections.Generic;

namespace Roguelike.Utilities.Extensions
{
    public static class ExceptionCheckingExtensions
    {
        #region Fields
        const string NoElementsExceptionMessage = "Sequence contains no elements";
        #endregion

        #region Methods
        public static T CheckForNullException<T>(this T argument, string paramName)
        {
            if (argument == null)
                throw new ArgumentNullException(paramName);
            return argument;
        }

        public static IEnumerable<T> CheckForNoElementsException<T>(this IEnumerable<T> argument, int elementsCount)
        {
            if (elementsCount <= 0)
                throw new InvalidOperationException(NoElementsExceptionMessage);
            return argument;
        }
        #endregion
    }
}