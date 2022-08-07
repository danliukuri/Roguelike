using System;
using UnityEngine;

namespace Roguelike.Core.Information
{
    public class MovingEventArgs : EventArgs
    {
        #region Properties
        public Transform Element { set; get; }
        public int ElementRoomIndex { set; get; }
        public bool IsMovePossible { set; get; }
        public Vector3 Destination { set; get; }
        #endregion
    }
}