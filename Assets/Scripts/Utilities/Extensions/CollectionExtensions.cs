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
        /// <param name="source">Sequence of items.</param>
        /// <returns>A random item from the sequence.</returns>
        public static T Random<T>(this ICollection<T> source) => source
            .CheckForNullException(nameof(source))
            .CheckForNoElementsException(source.Count)
            .ElementAt(source.RandomIndex(true));

        /// <summary>
        /// Return an index of a random item from the sequence.
        /// </summary>
        /// <typeparam name="T">Type of sequence's items.</typeparam>
        /// <param name="source">Sequence of items.</param>
        /// <param name="isSourceChecked">If the source has already been checked for possible exceptions.</param>
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
        /// <remarks>
        /// The greater the weight of the item among others, the more likely it is to be selected.
        /// </remarks>
        /// <typeparam name="T">Type of sequence's items values.</typeparam>
        /// <param name="source">Sequence of <see cref="WeightAndValue{T}"/> items.</param>
        /// <returns>A random item based on weights from the sequence.</returns>
        public static WeightAndValue<T> RandomBasedOnWeights<T>(this ICollection<WeightAndValue<T>> source)
        {
            source.CheckForNullException(nameof(source)).CheckForNoElementsException(source.Count);

            const int MinWeight = 0;
            int maxWeight = source.Sum(item => item.Weight);
            int currentWeight = UnityEngine.Random.Range(MinWeight, maxWeight);

            return source.First(item => (currentWeight -= item.Weight) < MinWeight);
        }
        #endregion
    }
}