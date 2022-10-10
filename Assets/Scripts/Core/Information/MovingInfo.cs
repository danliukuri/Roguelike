using System;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Core.Information
{
    public class MovingInfo
    {
        #region Properties
        public List<Transform>[] ElementsByRoom { get; set; }
        public MovingEventArgs Args { get; set; }
        #endregion
        
        #region Events
        public EventHandler<MovingEventArgs> Event { get; set; }
        #endregion
    }
}