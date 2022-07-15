using System;
using UnityEngine;

namespace Roguelike.Core.Information
{
    public class MovingEventArgs : EventArgs
    {
        #region Properties
        public Transform ElementWhichEntityIsMovingTo { set; get; }
        public bool IsMovePossible { set; get; }
        public Vector3 Destination { set; get; }
        #endregion
    }
}