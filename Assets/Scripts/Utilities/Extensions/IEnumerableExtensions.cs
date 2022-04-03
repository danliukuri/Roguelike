using System.Collections.Generic;
using System.Linq;

namespace Roguelike.Utilities.Extensions
{
    public static class IEnumerableExtensions
    {
        #region Methods
        /// <summary>
        /// Return a random item from the sequence.
        /// </summary>
        public static T Random<T>(this IEnumerable<T> sourse) => sourse
            .CheckForNullException(nameof(sourse))
            .CheckForNoElementsException(sourse.Count())
            .ElementAt(sourse.RandomIndex(true));

        /// <summary>
        /// Return an index of random item from the sequence.
        /// </summary>
        public static int RandomIndex<T>(this IEnumerable<T> sourse) => sourse.RandomIndex(false);
        static int RandomIndex<T>(this IEnumerable<T> sourse, bool isSourseChecked = false)
        {
            if (!isSourseChecked)
                sourse.CheckForNullException(nameof(sourse)).CheckForNoElementsException(sourse.Count());

            return UnityEngine.Random.Range(0, sourse.Count());
        }

        /// <summary>
        /// Return a random item based on weights from the sequence.
        /// </summary>
        /// <remarks>
        /// The greater the weight of the item among others, the more likely it is to be selected.
        /// </remarks>
        public static WeightAndValue<T> RandomBasedOnWeights<T>(this IEnumerable<WeightAndValue<T>> sourse)
        {
            sourse.CheckForNullException(nameof(sourse)).CheckForNoElementsException(sourse.Count());

            const int MinWeight = 0;
            int MaxWeight = sourse.Sum(item => item.Weight);
            int currentWeight = UnityEngine.Random.Range(MinWeight, MaxWeight);

            return sourse.First(item => (currentWeight -= item.Weight) < MinWeight);
        }
        #endregion
    }
}