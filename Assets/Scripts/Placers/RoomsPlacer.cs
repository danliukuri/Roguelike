using Roguelike.Core.Entities;
using Roguelike.Core.Factories;
using Roguelike.Core.Placers;
using Roguelike.Utilities.Extensions;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Placers
{
    public class RoomsPlacer : IRoomsPlacer
    {
        #region Fields
        readonly List<IGameObjectFactory> roomsFactories;
        #endregion

        #region Methods
        public RoomsPlacer(List<IGameObjectFactory> roomsFactories) => this.roomsFactories = roomsFactories;
        public List<Room> Place(Vector3 firstRoomPosition, int count, Transform parent)
        {
            List<Room> rooms = new List<Room>();
            List<Vector3> roomPositions = new List<Vector3>();
            List<Vector3> freePositions = new List<Vector3> { firstRoomPosition };

            for (int i = 0; i < count; i++)
            {
                GameObject roomGameObject = GetRoom(i);

                Transform roomTransform = roomGameObject.transform;
                roomTransform.SetParent(parent, false);

                Vector3 roomPosition = freePositions.Random();
                freePositions.Remove(roomPosition);
                roomPositions.Add(roomPosition);

                Room room = roomGameObject.GetComponent<Room>();
                room.SetNewPositionAndSizeBounds(roomPosition);
                rooms.Add(room);

                roomGameObject.SetActive(true);

                AddNewFreePositions(roomPositions, freePositions, roomPosition, room.Size);
            }
            
            CreatePassagesBetweenRooms(rooms);
            return rooms;
        }

        GameObject GetRoom(int roomIndex)
        {  
            const int firstRoomFactoryIndex = 0;
            int lastRoomFactoryIndex = roomsFactories.Count - 1;
            
            int randomRoomFactoryIndexExceptFirstAndLast =
                Random.Range(firstRoomFactoryIndex + 1, lastRoomFactoryIndex);
                
            int roomFactoryIndex =
                roomIndex == firstRoomFactoryIndex ? firstRoomFactoryIndex :
                roomIndex == lastRoomFactoryIndex ? lastRoomFactoryIndex :
                randomRoomFactoryIndexExceptFirstAndLast;

            return roomsFactories[roomFactoryIndex].GetGameObject();
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