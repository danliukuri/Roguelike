using System.Collections.Generic;
using Roguelike.Core.Entities;
using Roguelike.Core.Factories;
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

        public List<Room> Place(int count, Vector3 firstRoomPosition = default)
        {
            List<Room> rooms = new List<Room>();
            List<Vector3> roomPositions = new List<Vector3>();
            List<Vector3> freePositions = new List<Vector3> { firstRoomPosition };

            for (int i = 0; i < count; i++)
            {
                Room room = GetRoom(i);
                rooms.Add(room);
                
                room.transform.SetParent(parent, false);

                Vector3 roomPosition = freePositions.Random();
                freePositions.Remove(roomPosition);
                roomPositions.Add(roomPosition);
                
                room.SetNewPositionAndSizeBounds(roomPosition);
                
                AddNewFreePositions(roomPositions, freePositions, roomPosition, room.Size);
            }
            
            CreatePassagesBetweenRooms(rooms);
            return rooms;
        }

        Room GetRoom(int roomIndex)
        {  
            const int firstRoomFactoryIndex = 0;
            int lastRoomFactoryIndex = roomsFactories.Length - 1;
            
            int randomRoomFactoryIndexExceptFirstAndLast =
                Random.Range(firstRoomFactoryIndex + 1, lastRoomFactoryIndex);
                
            int roomFactoryIndex =
                roomIndex == firstRoomFactoryIndex ? firstRoomFactoryIndex :
                roomIndex == lastRoomFactoryIndex ? lastRoomFactoryIndex :
                randomRoomFactoryIndexExceptFirstAndLast;

            return roomsFactories[roomFactoryIndex].GetRoom();
        }
        static void AddNewFreePositions(ICollection<Vector3> roomPositions, ICollection<Vector3> freePositions,
            Vector3 currentRoomPosition, int roomSize)
        {
            foreach (var direction in Room.Directions)
            {
                Vector3 newFreePosition = currentRoomPosition + direction * roomSize;
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