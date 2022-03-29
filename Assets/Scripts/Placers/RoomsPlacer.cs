using Roguelike.Core.Entities;
using Roguelike.Core.Factories;
using Roguelike.Core.Placers;
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
            List<Room> rooms = new List<Room>();
            List<Vector3> roomPositions = new List<Vector3>();
            List<Vector3> freePositions = new List<Vector3>() { firstRoomPosition };
            for (int i = 0; i < count; i++)
            {
                GameObject roomGameObject = roomsFactories[Random.Range(0, roomsFactories.Count)].GetRoom();
                Transform roomTransform = roomGameObject.transform;
                roomTransform.SetParent(parent);

                Vector3 randomFreePosition = freePositions[Random.Range(0, freePositions.Count)];
                freePositions.Remove(randomFreePosition);
                Vector3 roomPosition = roomTransform.position = randomFreePosition;
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