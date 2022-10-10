using System;
using UnityEngine;

namespace Roguelike.Utilities.Generic
{
    [Serializable]
    public class WeightAndValue<T>
    {
        #region Properties
        public T Value { get => value; set => this.value = value; }
        public int Weight { get => weight; set => weight = value < MinWeight ? MinWeight : value; }
        #endregion
        
        #region Fields
        [SerializeField] T value;
        [SerializeField, Min(MinWeight)] int weight;
        
        const int MinWeight = default;
        #endregion
    }
}