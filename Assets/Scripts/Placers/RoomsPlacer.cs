using System.Collections.Generic;
using System.Linq;
using Roguelike.Core.Entities;
using Roguelike.Core.Factories;
using Roguelike.Core.Information;
using Roguelike.Core.Placers;
using Roguelike.Utilities.Extensions;
using UnityEngine;

namespace Roguelike.Placers
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
            foreach (var room in rooms)
            {
                room.transform.SetParent(parent, false);
                Vector3 roomPosition = freePositions.Random();
                room.SetNewPositionAndSizeBounds(roomPosition);
                
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
            int maxNumberOfRooms = levelSettings.NumberOfRooms;
            List<Room> rooms = GetRoomsWithRequiredMarkers(markersInfo, maxNumberOfRooms);

            IRoomFactory[] availableFactories = roomsFactories
                .Where(factory => markersInfo
                    .Where(elementsCount => elementsCount.GetActualCount(factory.Sample) > 0)
                    .All(elementsCount => elementsCount.RelatedRoomsMaxCount > 0))
                .ToArray();
            
            Room lastRoom = rooms.Last();
            rooms.Remove(lastRoom);
            while (rooms.Count + 1 < maxNumberOfRooms)
                rooms.Add(availableFactories.Random().GetRoom());
            rooms.Add(lastRoom);
            return rooms;
        }
        List<Room> GetRoomsWithRequiredMarkers(RoomElementMarkersInfo[] markersInfo, int maxNumberOfRooms)
        {
            List<Room> rooms = new List<Room>(maxNumberOfRooms);
            for (int i = 0; i < markersInfo.Length; i++)
                while (markersInfo[i].RequiredCount > 0 &&
                       markersInfo[i].RelatedRoomsMaxCount > 0 && rooms.Count < maxNumberOfRooms)
                {
                    IRoomFactory factory = roomsFactories
                        .Where(factory => markersInfo[i].GetActualCount(factory.Sample) > 0).ToArray().Random();

                    for (int j = 0; j < markersInfo.Length; j++)
                    {
                        markersInfo[j].RequiredCount -= markersInfo[j].GetActualCount(factory.Sample);
                        markersInfo[j].RelatedRoomsMaxCount--;
                    }
                    rooms.Add(factory.GetRoom());
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