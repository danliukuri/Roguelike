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
        readonly List<IRoomFactory> roomsFactories;
        #endregion

        #region Methods
        public RoomsPlacer(List<IRoomFactory> roomsFactories) => this.roomsFactories = roomsFactories;
        public List<Room> Place(Vector3 firstRoomPosition, int count, Transform parent)
        {
            int firstRoomFactoryIndex = 0;
            int lastRoomFactoryIndex = roomsFactories.Count - 1;

            List<Room> rooms = new List<Room>();
            List<Vector3> roomPositions = new List<Vector3>();
            List<Vector3> freePositions = new List<Vector3>() { firstRoomPosition };

            for (int i = 0; i < count; i++)
            {
                int randomRoomFactoryIndexExceptFirstAndLast =
                    Random.Range(firstRoomFactoryIndex + 1, lastRoomFactoryIndex);
                
                int roomFactoryIndex =
                    i == firstRoomFactoryIndex ? firstRoomFactoryIndex :
                    i == lastRoomFactoryIndex ? lastRoomFactoryIndex :
                    randomRoomFactoryIndexExceptFirstAndLast;

                GameObject roomGameObject = roomsFactories[roomFactoryIndex].GetRoom();

                Transform roomTransform = roomGameObject.transform;
                roomTransform.SetParent(parent);

                Vector3 roomPosition = roomTransform.position = freePositions.Random();
                freePositions.Remove(roomPosition);
                roomPositions.Add(roomPosition);

                Room room = roomGameObject.GetComponent<Room>();
                rooms.Add(room);

                roomGameObject.SetActive(true);

                for (int j = 0; j < Room.Directions.Length; j++)
                {
                    Vector3 newFreePosition = roomPosition + Room.Directions[j] * room.Size;
                    if (!freePositions.Contains(newFreePosition) && !roomPositions.Contains(newFreePosition))
                        freePositions.Add(newFreePosition);
                }
            }
            return rooms;
        }
        #endregion
    }
}