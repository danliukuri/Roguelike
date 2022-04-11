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
        /// <typeparam name="T">Type of sequence's items.</typeparam>
        /// <param name="sourse">Sequence of items.</param>
        /// <returns>A random item from the sequence.</returns>
        public static T Random<T>(this ICollection<T> sourse) => sourse
            .CheckForNullException(nameof(sourse))
            .CheckForNoElementsException(sourse.Count)
            .ElementAt(sourse.RandomIndex(true));

        /// <summary>
        /// Return an index of random item from the sequence.
        /// </summary>
        /// <typeparam name="T">Type of sequence's items.</typeparam>
        /// <param name="sourse">Sequence of items.</param>
        /// <returns>An index of random item from the sequence.</returns>
        public static int RandomIndex<T>(this ICollection<T> sourse) => sourse.RandomIndex(false);
        static int RandomIndex<T>(this ICollection<T> sourse, bool isSourseChecked = false)
        {
            if (!isSourseChecked)
                sourse.CheckForNullException(nameof(sourse)).CheckForNoElementsException(sourse.Count);

            return UnityEngine.Random.Range(0, sourse.Count);
        }

        /// <summary>
        /// Return a random item based on weights from the sequence.
        /// </summary>
        /// <remarks>
        /// The greater the weight of the item among others, the more likely it is to be selected.
        /// </remarks>
        /// <typeparam name="T">Type of sequence's items values.</typeparam>
        /// <param name="sourse">Sequence of <see cref="WeightAndValue{T}"/> items.</param>
        /// <returns>A random item based on weights from the sequence.</returns>
        public static WeightAndValue<T> RandomBasedOnWeights<T>(this ICollection<WeightAndValue<T>> sourse)
        {
            sourse.CheckForNullException(nameof(sourse)).CheckForNoElementsException(sourse.Count);

            const int MinWeight = 0;
            int maxWeight = sourse.Sum(item => item.Weight);
            int currentWeight = UnityEngine.Random.Range(MinWeight, maxWeight);

            return sourse.First(item => (currentWeight -= item.Weight) < MinWeight);
        }
        #endregion
    }
}