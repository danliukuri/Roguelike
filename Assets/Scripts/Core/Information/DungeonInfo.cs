using System;
using System.Collections.Generic;
using System.Linq;
using Roguelike.Core.Entities;
using UnityEngine;

namespace Roguelike.Core.Information
{
    public class DungeonInfo
    {
        #region Properties
        public List<Room> Rooms { get; set; }
        public List<Transform>[] WallsByRoom { get; set; }
        public List<Transform>[] PlayersByRoom { get; set; }
        public List<Transform>[] EnemiesByRoom { get; set; }
        public List<Transform>[] KeysByRoom { get; set; }
        public List<Transform>[] DoorsByRoom { get; set; }
        public List<Transform>[] ExitsByRoom { get; set; }
        
        public int PlayerRoomIndex { get; private set; }
        #endregion

        #region Fields
        public delegate bool PlayerToElementMovingEventHandler(Transform element);
        public event PlayerToElementMovingEventHandler PlayerToWallMoving;
        public event PlayerToElementMovingEventHandler PlayerToKeyMoving;
        public event Action<int> PlayerRoomIndexChanged;
        #endregion

        #region Methods
        public List<Transform>[] GetAllMarkersByRoom(Func<Room, IEnumerable<Transform>> getMarkersFromRoom)
        {
            List<Transform>[] allMarkersByRoom = new List<Transform>[Rooms.Count];
            for (int i = 0; i < Rooms.Count; i++)
                allMarkersByRoom[i] = getMarkersFromRoom.Invoke(Rooms[i]).ToList();
            return allMarkersByRoom;
        }
        public int GetRoomIndex(Vector3 position)
        {
            for (int roomIndex = 0; roomIndex < Rooms.Count; roomIndex++)
                if (Rooms[roomIndex].IsInsideSizeBounds(position))
                    return roomIndex;
            throw new InvalidOperationException("There is no room with such position");
        }
        
        public bool IsPlayerMovePossible(Vector3 destination)
        {
            int roomIndex = GetRoomIndex(destination);
            if (PlayerRoomIndex != roomIndex)
            {
                PlayerRoomIndex = roomIndex;
                PlayerRoomIndexChanged?.Invoke(roomIndex);
            }
            
            var roomElementsInfo = new (List<Transform>[] ElementsByRoom,
                PlayerToElementMovingEventHandler PlayerToElementMoving)[]
                { 
                    (KeysByRoom, PlayerToKeyMoving),
                    (WallsByRoom, PlayerToWallMoving),
                };
            
            bool isMovePossible = true;
            foreach (var (elementsByRoom, playerToElementMoving) in roomElementsInfo)
            {
                Transform elementWhichPlayerMoveTo = elementsByRoom[roomIndex]?
                    .FirstOrDefault(element => element.position == destination);
                bool isElementOnPosition = elementWhichPlayerMoveTo != default;
            
                if (playerToElementMoving != default && isElementOnPosition) 
                    isMovePossible = playerToElementMoving.Invoke(elementWhichPlayerMoveTo);
                
                if(isElementOnPosition)
                    break;
            }
            return isMovePossible;
        }
        #endregion
    }
}