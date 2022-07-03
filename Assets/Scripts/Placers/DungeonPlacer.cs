using System;
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
        readonly IWallsPlacer wallsPlacer;
        readonly IDoorsPlacer doorsPlacer;
        readonly IExitsPlacer exitsPlacer;
        readonly IPlayersPlacer playersPlacer;
        
        List<Room> rooms;
        #endregion

        #region Methods
        public DungeonPlacer(IRoomsPlacer roomsPlacer, IWallsPlacer wallsPlacer,
            IDoorsPlacer doorsPlacer, IExitsPlacer exitsPlacer, IPlayersPlacer playersPlacer)
        {
            this.roomsPlacer = roomsPlacer;
            this.wallsPlacer = wallsPlacer;
            this.doorsPlacer = doorsPlacer;
            this.exitsPlacer = exitsPlacer;
            this.playersPlacer = playersPlacer;
        }

        public void Place(Vector3 firstRoomPosition, int roomsCount, Transform parent)
        {
            rooms = roomsPlacer.Place(firstRoomPosition, roomsCount, parent);
            CreatePassagesBetweenRooms();
            wallsPlacer.Place(GetAllObjectsMarkers(room => room.AllWallsMarkers));
            doorsPlacer.Place(GetAllObjectsMarkers(room => room.AllDoorsMarkers));
            exitsPlacer.Place(GetAllObjectsMarkers(room => room.AllExitsMarkers));
            playersPlacer.Place(GetAllObjectsMarkers(room => room.AllPlayersMarkers));
        }
        
        void CreatePassagesBetweenRooms()
        {
            for (int i = 0; i < rooms?.Count; i++)
                for (int j = i; j < rooms.Count; j++)
                    rooms[i].TryToCreatePassageTo(rooms[j]);
        }

        List<Transform> GetAllObjectsMarkers(Func<Room, IEnumerable<Transform>> getObjectsMarkersFromRoom)
        {
            List<Transform> allObjectsMarkers = new List<Transform>();
            rooms.ForEach(room => allObjectsMarkers.AddRange(getObjectsMarkersFromRoom.Invoke(room)));
            return allObjectsMarkers;
        }
        #endregion
    }
}