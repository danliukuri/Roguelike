using Roguelike.Core.Entities;
using Roguelike.Core.Placers;
using UnityEngine;
using Zenject;

namespace Roguelike.Placers
{
    public class DungeonPlacer : IDungeonPlacer
    {
        #region Fields
        readonly IRoomsPlacer roomsPlacer;
        
        readonly IRoomElementsPlacer wallsPlacer;
        readonly IRoomElementsPlacer playersPlacer;
        readonly IRoomElementsPlacer doorsPlacer;
        readonly IRoomElementsPlacer exitsPlacer;
        
        readonly DungeonInfo dungeonInfo;
        #endregion

        #region Methods
        public DungeonPlacer(IRoomsPlacer roomsPlacer,
            [Inject(Id = RoomElementType.Wall)] IRoomElementsPlacer wallsPlacer,
            [Inject(Id = RoomElementType.Player)] IRoomElementsPlacer playersPlacer,
            [Inject(Id = RoomElementType.Door)] IRoomElementsPlacer doorsPlacer, 
            [Inject(Id = RoomElementType.Exit)] IRoomElementsPlacer exitsPlacer,
            DungeonInfo dungeonInfo)
        {
            this.roomsPlacer = roomsPlacer;
            
            this.wallsPlacer = wallsPlacer;
            this.playersPlacer = playersPlacer;
            this.doorsPlacer = doorsPlacer;
            this.exitsPlacer = exitsPlacer;
            
            this.dungeonInfo = dungeonInfo;
        }
        
        public void Place(Vector3 firstRoomPosition ,int roomsCount, Transform parent)
        {
            dungeonInfo.Rooms = roomsPlacer.Place(firstRoomPosition, roomsCount, parent);

            dungeonInfo.WallsByRoom =
                wallsPlacer.PlaceAll(dungeonInfo.GetAllMarkersByRoom(room => room.AllWallsMarkers));
            dungeonInfo.PlayersByRoom =
                playersPlacer.PlaceOneRandom(dungeonInfo.GetAllMarkersByRoom(room => room.AllPlayerMarkers));
            dungeonInfo.DoorsByRoom =
                doorsPlacer.PlaceAll(dungeonInfo.GetAllMarkersByRoom(room => room.AllDoorMarkers));
            dungeonInfo.ExitsByRoom =
                exitsPlacer.PlaceOneRandom(dungeonInfo.GetAllMarkersByRoom(room => room.AllExitMarkers));
        }
        #endregion
    }
}