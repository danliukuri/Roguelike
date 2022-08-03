using System;
using Roguelike.Core.Entities;

namespace Roguelike.Core.Information
{
    public class RoomElementMarkersInfo
    {
        #region Properties
        public Func<Room, int> GetActualCount { get; set; }
        public int RequiredCount { get; set; }
        public int RelatedRoomsMaxCount { get; set; } = int.MaxValue;
        #endregion
    }
}