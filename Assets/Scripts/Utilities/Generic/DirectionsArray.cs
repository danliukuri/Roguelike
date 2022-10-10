using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Roguelike.Utilities.Extensions.Generic;
using UnityEngine;

namespace Roguelike.Utilities.Generic
{
    [Serializable]
    public class DirectionsArray<T> : IEnumerable<T>
    {
        #region Properties
        public T North { get => this[NorthDirectionIndex]; set => this[NorthDirectionIndex] = value; }
        public T West { get => this[WestDirectionIndex]; set => this[WestDirectionIndex] = value; }
        public T South { get => this[SouthDirectionIndex]; set => this[SouthDirectionIndex] = value; }
        public T East { get => this[EastDirectionIndex]; set => this[EastDirectionIndex] = value; }
        public T this[int directionIndex] 
        { 
            get => GetDirectionValues()[directionIndex];
            set => GetDirectionValues()[directionIndex] = value;
        }
        #endregion
        
        #region Fields
        const int NumberOfDirections = 4;
        const int NumberOfIterationsToOppositeDirection = NumberOfDirections / 2;
        const int NorthDirectionIndex = 0, WestDirectionIndex = 1, SouthDirectionIndex = 2, EastDirectionIndex = 3;
        
        [SerializeField] public T north, west, south, east;
        T[] directionValues;
        #endregion
        
        #region Methods
        T[] GetDirectionValues() => directionValues ??= new[] { north, west, south, east };
        public T GetOppositeDirectionValue(T directionValue) => this[OppositeDirectionIndexOf(directionValue)];
        
        public int DirectionIndexOf(T directionValue) => Array.IndexOf(GetDirectionValues(), directionValue);
        public int OppositeDirectionIndexOf(T directionValue) =>
            OppositeDirectionIndexTo(DirectionIndexOf(directionValue));
        public int OppositeDirectionIndexTo(int directionIndex) =>
            (directionIndex + NumberOfIterationsToOppositeDirection) % NumberOfDirections;
        
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetDirectionValues().AsEnumerable().GetEnumerator();
        public IEnumerator GetEnumerator() => GetDirectionValues().GetEnumerator();
        public void ChangeOrientationToEast() => GetDirectionValues().ShiftedRight();
        #endregion
    }
}