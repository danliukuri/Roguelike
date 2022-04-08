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

        List<Room> rooms;
        #endregion

        #region Methods
        public DungeonPlacer(IRoomsPlacer roomsPlacer, IWallsPlacer wallsPlacer,
            IDoorsPlacer doorsPlacer, IExitsPlacer exitsPlacer)
        {
            this.roomsPlacer = roomsPlacer;
            this.wallsPlacer = wallsPlacer;
            this.doorsPlacer = doorsPlacer;
            this.exitsPlacer = exitsPlacer;
        }

        public void Place(Vector3 firstRoomPosition, int roomsCount, Transform parent)
        {
            rooms = roomsPlacer.Place(firstRoomPosition, roomsCount, parent);
            CreatePassagesBetweenRooms();
            PlaceWalls();
            PlaceDoors();
            PlaceExits();
        }

        void CreatePassagesBetweenRooms()
        {
            for (int i = 0; i < rooms?.Count; i++)
                for (int j = i; j < rooms.Count; j++)
                    rooms[i].TryToCreatePassageTo(rooms[j]);
        }

        void PlaceWalls()
        {
            List<Transform> allWallsMarkers = new List<Transform>();
            rooms.ForEach(room => allWallsMarkers.AddRange(room.AllWallsMarkers));
            wallsPlacer.Place(allWallsMarkers);
        }
        void PlaceExits()
        {
            List<Transform> allExitsMarkers = new List<Transform>();
            rooms.ForEach(room => allExitsMarkers.AddRange(room.AllExitsMarkers));
            exitsPlacer.Place(allExitsMarkers);
        }
        void PlaceDoors()
        {
            List<Transform> allDoorsMarkers = new List<Transform>();
            rooms.ForEach(room => allDoorsMarkers.AddRange(room.AllDoorsMarkers));
            doorsPlacer.Place(allDoorsMarkers);
        }
        #endregion
    }
}