using Roguelike.Core.Entities;
using Roguelike.Core.Placers;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Placers
{
    public class DungeonPlacer : IDungeonPlacer
    {
        #region Fields
        readonly IRoomsPlacer roomsPlacer;

        List<Room> rooms;
        #endregion

        #region Methods
        public DungeonPlacer(IRoomsPlacer roomsPlacer) => this.roomsPlacer = roomsPlacer;

        public void Place(Vector3 firstRoomPosition, int roomsCount, Transform parent)
        {
            rooms = roomsPlacer.Place(firstRoomPosition, roomsCount, parent);
            CreatePassagesBetweenRooms();
        }
        void CreatePassagesBetweenRooms()
        {
            for (int i = 0; i < rooms?.Count; i++)
                for (int j = i; j < rooms.Count; j++)
                    rooms[i].TryToCreatePassageTo(rooms[j]);
        }
        #endregion
    }
}