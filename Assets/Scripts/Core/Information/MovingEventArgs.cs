using System;
using Roguelike.Utilities.Generic.Characteristics;
using UnityEngine;

namespace Roguelike.Core.Information
{
    public class MovingEventArgs : EventArgs, IShallowCopyable<MovingEventArgs>
    {
        #region Properties
        public Transform Element { set; get; }
        public int ElementRoomIndex { set; get; }
        public bool IsMovePossible { set; get; }
        public Vector3 Destination { set; get; }
        #endregion
        
        #region Methods
        public MovingEventArgs ShallowCopy() => (MovingEventArgs)MemberwiseClone();
        #endregion
    }
}