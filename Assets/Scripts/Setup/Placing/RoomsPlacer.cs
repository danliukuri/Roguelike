using System.Collections.Generic;
using System.Linq;
using Roguelike.Core.Entities;
using Roguelike.Core.Information;
using Roguelike.Core.Setup.Factories;
using Roguelike.Core.Setup.Placing;
using Roguelike.Utilities.Extensions.Generic;
using Roguelike.Utilities.Generic.Information;
using UnityEngine;

namespace Roguelike.Setup.Placing
{
    public class RoomsPlacer : IRoomsPlacer
    {
        #region Fields
        readonly IRoomFactory[] roomsFactories;
        readonly Transform parent;
        #endregion
        
        #region Methods
        public RoomsPlacer(IRoomFactory[] roomsFactories, Transform parent = default)
        {
            this.roomsFactories = roomsFactories;
            this.parent = parent;
        }
        
        public List<Room> Place(LevelSettings levelSettings, Vector3 firstRoomPosition = default)
        {
            List<Vector3> roomPositions = new List<Vector3>();
            List<Vector3> freePositions = new List<Vector3> { firstRoomPosition };
            List<Room> rooms = GetRooms(levelSettings);
            foreach (Room room in rooms)
            {
                room.transform.SetParent(parent, false);
                Vector3 roomPosition = room.Position = freePositions.Random();
                
                roomPositions.Add(roomPosition);
                freePositions.Remove(roomPosition);
                AddNewFreePositions(roomPositions, freePositions, roomPosition, room.Size);
            }
            CreatePassagesBetweenRooms(rooms);
            return rooms;
        }
        
        List<Room> GetRooms(LevelSettings levelSettings)
        {
            RoomElementMarkersInfo[] markersInfo = levelSettings.RoomElementMarkersInfo.ShallowCopy().ToArray();
            Dictionary<OrdinalPosition, List<Room>> roomsByPlacingPosition = GetRequiredRooms(markersInfo);
            
            IRoomFactory[] availableFactories = roomsFactories
                .Where(factory => markersInfo
                    .Where(elementsCount =>
                        elementsCount.GetActualCount(factory.Sample) > RoomElementMarkersInfo.MinElementMarkersCount)
                    .All(elementsCount => 
                        elementsCount.RelatedRoomsMaxCount > RoomElementMarkersInfo.MinRelatedRoomsCount))
                .ToArray();
            
            int nonRequiredRoomsCount = levelSettings.NumberOfRooms - markersInfo.Length;
            List<Room> nonRequiredRooms = new List<Room>(nonRequiredRoomsCount);
            for (int i = 0; i < nonRequiredRoomsCount; i++)
                nonRequiredRooms.Add(availableFactories.Random().GetRoom());
            
            roomsByPlacingPosition[OrdinalPosition.Any].AddRange(nonRequiredRooms);
            return roomsByPlacingPosition.Values.SelectMany(rooms => rooms).ToList();
        }
        Dictionary<OrdinalPosition, List<Room>> GetRequiredRooms(IEnumerable<RoomElementMarkersInfo> markersInfo)
        {
            Dictionary<OrdinalPosition, List<Room>> rooms = new Dictionary<OrdinalPosition, List<Room>>();
            foreach (RoomElementMarkersInfo currentMarkersInfo in markersInfo)
            {
                IRoomFactory factory = roomsFactories.Where(factory =>
                        currentMarkersInfo.GetActualCount(factory.Sample) >= currentMarkersInfo.RequiredCount)
                    .ToArray().Random();
                
                currentMarkersInfo.RequiredCount -= currentMarkersInfo.GetActualCount(factory.Sample);
                if (currentMarkersInfo.RequiredCount == RoomElementMarkersInfo.MinElementMarkersCount)
                {
                    rooms.AddEvenIfEmpty(currentMarkersInfo.RelatedRoomsPlacingOrderNumber, factory.GetRoom());
                    currentMarkersInfo.RelatedRoomsMaxCount--;
                }
            }
            return rooms;
        }
        
        static void AddNewFreePositions(ICollection<Vector3> roomPositions, ICollection<Vector3> freePositions,
            Vector3 currentRoomPosition, int roomSize)
        {
            foreach (Vector3 neighborDirection in Room.NeighborDirections)
            {
                Vector3 newFreePosition = currentRoomPosition + neighborDirection * roomSize;
                if (!freePositions.Contains(newFreePosition) && !roomPositions.Contains(newFreePosition))
                    freePositions.Add(newFreePosition);
            }
        }
        static void CreatePassagesBetweenRooms(IReadOnlyList<Room> rooms)
        {
            for (int i = 0; i < rooms?.Count; i++)
                for (int j = i; j < rooms.Count; j++)
                    rooms[i].TryToCreatePassageTo(rooms[j]);
        }
        #endregion
    }
}