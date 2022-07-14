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
        #endregion
    }
}