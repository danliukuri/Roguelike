using System;
using Roguelike.Core.Entities;
using Roguelike.Utilities;

namespace Roguelike.Core.Information
{
    public class RoomElementMarkersInfo : IShallowCopyable<RoomElementMarkersInfo>
    {
        #region Properties
        public Func<Room, int> GetActualCount { get; set; }
        public int RequiredCount { get; set; }
        public int RelatedRoomsMaxCount { get; set; } = int.MaxValue;
        #endregion
        
        #region Methods
        public RoomElementMarkersInfo ShallowCopy() => (RoomElementMarkersInfo)MemberwiseClone();
        #endregion
    }
}