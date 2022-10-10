using System.Collections.Generic;
using Roguelike.Core.Information;
using Roguelike.Core.Setup.Placing;
using UnityEngine;
using Zenject;

namespace Roguelike.Setup.Placing
{
    public class DungeonPlacer : IDungeonPlacer
    {
        #region Fields
        readonly DungeonInfo dungeonInfo;
        readonly IRoomsPlacer roomsPlacer;
        
        readonly IRoomElementsPlacer wallsPlacer;
        readonly IRoomElementsPlacer playersPlacer;
        readonly IRoomElementsPlacer enemiesPlacer;
        readonly IRoomElementsPlacer keysPlacer;
        readonly IRoomElementsPlacer doorsPlacer;
        readonly IRoomElementsPlacer exitsPlacer;
        #endregion
        
        #region Methods
        public DungeonPlacer(DungeonInfo dungeonInfo, IRoomsPlacer roomsPlacer,
            [Inject(Id = RoomElementType.Wall)] IRoomElementsPlacer wallsPlacer,
            [Inject(Id = EntityType.Player)] IRoomElementsPlacer playersPlacer,
            [Inject(Id = EntityType.Enemy)] IRoomElementsPlacer enemiesPlacer,
            [Inject(Id = RoomElementType.Key)] IRoomElementsPlacer keysPlacer,
            [Inject(Id = RoomElementType.Door)] IRoomElementsPlacer doorsPlacer, 
            [Inject(Id = RoomElementType.Exit)] IRoomElementsPlacer exitsPlacer)
        {
            this.dungeonInfo = dungeonInfo;
            this.roomsPlacer = roomsPlacer;
            
            this.wallsPlacer = wallsPlacer;
            this.playersPlacer = playersPlacer;
            this.enemiesPlacer = enemiesPlacer;
            this.keysPlacer = keysPlacer;
            this.doorsPlacer = doorsPlacer;
            this.exitsPlacer = exitsPlacer;
        }
        
        public void Place(LevelSettings levelSettings)
        {
            dungeonInfo.Rooms = roomsPlacer.Place(levelSettings);
            
            List<Transform>[] allWallMarkersByRoom = dungeonInfo.GetAllMarkersByRoom(room => room.AllWallsMarkers);
            List<Transform>[] allPlayerMarkersByRoom = dungeonInfo.GetAllMarkersByRoom(room => room.AllPlayerMarkers);
            List<Transform>[] allEnemyMarkersByRoom = dungeonInfo.GetAllMarkersByRoom(room => room.AllEnemyMarkers);
            List<Transform>[] allKeyMarkersByRoom = dungeonInfo.GetAllMarkersByRoom(room => room.AllItemMarkers);
            List<Transform>[] allDoorMarkersByRoom = dungeonInfo.GetAllMarkersByRoom(room => room.AllDoorMarkers);
            List<Transform>[] allExitMarkersByRoom = dungeonInfo.GetAllMarkersByRoom(room => room.AllExitMarkers);
            
            dungeonInfo.WallsByRoom = wallsPlacer.PlaceAll(allWallMarkersByRoom);
            dungeonInfo.PlayersByRoom = playersPlacer.PlaceRandom(allPlayerMarkersByRoom);
            dungeonInfo.EnemiesByRoom = enemiesPlacer.PlaceRandom(allEnemyMarkersByRoom, levelSettings.NumberOfEnemies);
            dungeonInfo.KeysByRoom = keysPlacer.PlaceRandom(allKeyMarkersByRoom, levelSettings.NumberOfKeys);
            dungeonInfo.DoorsByRoom = doorsPlacer.PlaceAll(allDoorMarkersByRoom);
            dungeonInfo.ExitsByRoom = exitsPlacer.PlaceRandom(allExitMarkersByRoom);
        }
        #endregion
    }
}