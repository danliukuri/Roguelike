using System.Collections.Generic;
using System.Linq;

namespace Roguelike.Utilities.Extensions
{
    public static class IEnumerableExtensions
    {
        #region Methods
        /// <summary>
        /// Gives a random item from the sequence.
        /// </summary>
        public static T Random<T>(this IEnumerable<T> sourse)
        {
            int randomIndex = UnityEngine.Random.Range(0, sourse.Count());
            return sourse.ElementAt(randomIndex);
        }

        /// <summary>
        /// Gives a random item based on weights from the sequence.
        /// </summary>
        /// <remarks>
        /// The greater the weight of the item among others, the more likely it is to be selected.
        /// </remarks>
        public static WeightAndValue<T> RandomBasedOnWeights<T>(this IEnumerable<WeightAndValue<T>> sourse)
        {
            int totalWeight = sourse.Sum(item => item.Weight);
            int currentWeight = UnityEngine.Random.Range(0, totalWeight);

            return sourse.First(item => (currentWeight += item.Weight) >= totalWeight);
        }
        #endregion
    }
}