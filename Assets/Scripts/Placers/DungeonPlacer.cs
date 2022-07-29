using System.Collections.Generic;
using Roguelike.Core.Information;
using Roguelike.Core.Placers;
using UnityEngine;
using Zenject;

namespace Roguelike.Placers
{
    public class DungeonPlacer : IDungeonPlacer
    {
        #region Fields
        readonly DungeonInfo dungeonInfo;
        readonly IRoomsPlacer roomsPlacer;
        
        readonly IRoomElementsPlacer wallsPlacer;
        readonly IRoomElementsPlacer playersPlacer;
        readonly IRoomElementsPlacer keysPlacer;
        readonly IRoomElementsPlacer doorsPlacer;
        readonly IRoomElementsPlacer exitsPlacer;
        #endregion

        #region Methods
        public DungeonPlacer(DungeonInfo dungeonInfo, IRoomsPlacer roomsPlacer,
            [Inject(Id = RoomElementType.Wall)] IRoomElementsPlacer wallsPlacer,
            [Inject(Id = RoomElementType.Player)] IRoomElementsPlacer playersPlacer,
            [Inject(Id = RoomElementType.Key)] IRoomElementsPlacer keysPlacer,
            [Inject(Id = RoomElementType.Door)] IRoomElementsPlacer doorsPlacer, 
            [Inject(Id = RoomElementType.Exit)] IRoomElementsPlacer exitsPlacer)
        {
            this.dungeonInfo = dungeonInfo;
            this.roomsPlacer = roomsPlacer;
            
            this.wallsPlacer = wallsPlacer;
            this.playersPlacer = playersPlacer;
            this.keysPlacer = keysPlacer;
            this.doorsPlacer = doorsPlacer;
            this.exitsPlacer = exitsPlacer;
        }
        
        public void Place(LevelSettings levelSettings)
        {
            dungeonInfo.Rooms = roomsPlacer.Place(default, levelSettings.NumberOfRooms,
                levelSettings.EnvironmentParent);
            
            List<Transform>[] allWallMarkersByRoom = dungeonInfo.GetAllMarkersByRoom(room => room.AllWallsMarkers);
            List<Transform>[] allPlayerMarkersByRoom = dungeonInfo.GetAllMarkersByRoom(room => room.AllPlayerMarkers);
            List<Transform>[] allKeyMarkersByRoom = dungeonInfo.GetAllMarkersByRoom(room => room.AllItemMarkers);
            List<Transform>[] allDoorMarkersByRoom = dungeonInfo.GetAllMarkersByRoom(room => room.AllDoorMarkers);
            List<Transform>[] allExitMarkersByRoom = dungeonInfo.GetAllMarkersByRoom(room => room.AllExitMarkers);
            
            dungeonInfo.WallsByRoom = wallsPlacer.PlaceAll(allWallMarkersByRoom);
            dungeonInfo.PlayersByRoom = playersPlacer.PlaceRandom(allPlayerMarkersByRoom);
            dungeonInfo.KeysByRoom = keysPlacer.PlaceRandom(allKeyMarkersByRoom, levelSettings.NumberOfKeys);
            dungeonInfo.DoorsByRoom = doorsPlacer.PlaceAll(allDoorMarkersByRoom);
            dungeonInfo.ExitsByRoom = exitsPlacer.PlaceRandom(allExitMarkersByRoom);
        }
        #endregion
    }
}