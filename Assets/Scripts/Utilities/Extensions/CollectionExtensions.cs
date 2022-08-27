﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Roguelike.Utilities.Extensions
{
    public static class CollectionExtensions
    {
        #region Methods
        /// <summary>
        /// Return a random item from the sequence.
        /// </summary>
        /// <param name="source">A sequence of items.</param>
        /// <typeparam name="T">The type of items of the sequence.</typeparam>
        /// <returns>A random item from the sequence.</returns>
        public static T Random<T>(this ICollection<T> source) => source
            .CheckForNullException(nameof(source))
            .CheckForNoElementsException(source.Count)
            .ElementAt(source.RandomIndex(true));
        
        /// <summary>
        /// Return an index of a random item from the sequence.
        /// </summary>
        /// <param name="source">A sequence of items.</param>
        /// <param name="isSourceChecked">If the source has already been checked for possible exceptions.</param>
        /// <typeparam name="T">The type of items of the sequence.</typeparam>
        /// <returns>An index of a random item from the sequence.</returns>
        public static int RandomIndex<T>(this ICollection<T> source, bool isSourceChecked = false)
        {
            if (!isSourceChecked)
                source.CheckForNullException(nameof(source)).CheckForNoElementsException(source.Count);

            return UnityEngine.Random.Range(0, source.Count);
        }
        
        /// <summary>
        /// Return a random item based on weights from the sequence.
        /// </summary>
        /// <param name="source">A sequence of <see cref="WeightAndValue{T}"/> items.</param>
        /// <typeparam name="T">The type of items of the sequence.</typeparam>
        /// <returns>A random item based on weights from the sequence.</returns>
        /// <remarks>
        /// The greater the weight of the item among others, the more likely it is to be selected.
        /// </remarks>
        public static WeightAndValue<T> RandomBasedOnWeights<T>(this ICollection<WeightAndValue<T>> source)
        {
            source.CheckForNullException(nameof(source)).CheckForNoElementsException(source.Count);

            const int minWeight = 0;
            int maxWeight = source.Sum(item => item.Weight);
            int currentWeight = UnityEngine.Random.Range(minWeight, maxWeight);

            return source.First(item => (currentWeight -= item.Weight) < minWeight);
        }
        
        /// <summary>
        /// Creates a new sequence with shallow copies of each item.
        /// </summary>
        /// <param name="source">A sequence of items.</param>
        /// <typeparam name="T">The type of items of the sequence, must implement interface
        /// <see cref="IShallowCopyable{T}"/>.</typeparam>
        /// <returns>A new sequence with shallow copies of each item.</returns>
        public static IEnumerable<T> ShallowCopy<T>(this IEnumerable<T> source) where T : IShallowCopyable<T> =>
            source?.Select(item => item.ShallowCopy());
        
        /// <summary>
        /// Shifts right array items. Modifies source array.
        /// </summary>
        /// <param name="source">An array of items.</param>
        /// <typeparam name="T">The type of items of the array.</typeparam>
        /// <returns>A shifted right source array.</returns>
        public static T[] ShiftedRight<T>(this T[] source)
        {
            source.CheckForNullException(nameof(source)).CheckForNoElementsException(source.Length);
            
            const int sourceIndex = 0;
            const int destinationIndex = 1;
            const int firstSourceItemIndex = 0;
            int lastSourceItemIndex = source.Length - 1;
            
            T lastItem = source[lastSourceItemIndex];
            Array.Copy(source, sourceIndex, source, destinationIndex, lastSourceItemIndex);
            source[firstSourceItemIndex] = lastItem;
            return source;
        }
        #endregion
    }
}