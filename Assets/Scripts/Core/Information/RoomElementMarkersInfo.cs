using System;
using Roguelike.Core.Entities;
using Roguelike.Utilities.Generic.Characteristics;
using Roguelike.Utilities.Generic.Information;

namespace Roguelike.Core.Information
{
    public class RoomElementMarkersInfo : IShallowCopyable<RoomElementMarkersInfo>
    {
        #region Properties
        public Func<Room, int> GetActualCount { get; set; }
        public int RequiredCount
        {
            get => requiredCount;
            set => requiredCount = value > MinElementMarkersCount ? value : MinElementMarkersCount;
        }
        public int RelatedRoomsMaxCount
        {
            get => relatedRoomsMaxCount;
            set => relatedRoomsMaxCount = value > MinRelatedRoomsCount ? value : MinRelatedRoomsCount;
        }
        public OrdinalPosition RelatedRoomsPlacingOrderNumber { get; set; } = OrdinalPosition.Any;
        #endregion
        
        #region Fields
        public const int MinRelatedRoomsCount = default;
        public const int MinElementMarkersCount = default;
        
        int requiredCount;
        int relatedRoomsMaxCount = int.MaxValue;
        #endregion
        
        #region Methods
        public RoomElementMarkersInfo ShallowCopy() => (RoomElementMarkersInfo)MemberwiseClone();
        #endregion
    }
}