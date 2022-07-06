using Roguelike.Core.Entities;
using Roguelike.Core.Placers;
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
        
        readonly DungeonInfo dungeonInfo;
        #endregion

        #region Methods
        public DungeonPlacer(IRoomsPlacer roomsPlacer, IWallsPlacer wallsPlacer, IDoorsPlacer doorsPlacer,
            IExitsPlacer exitsPlacer, IPlayersPlacer playersPlacer, DungeonInfo dungeonInfo)
        {
            this.roomsPlacer = roomsPlacer;
            this.wallsPlacer = wallsPlacer;
            this.doorsPlacer = doorsPlacer;
            this.exitsPlacer = exitsPlacer;
            this.playersPlacer = playersPlacer;
            this.dungeonInfo = dungeonInfo;
        }
        
        public void Place(Vector3 firstRoomPosition ,int roomsCount, Transform parent)
        {
            dungeonInfo.Rooms = roomsPlacer.Place(firstRoomPosition, roomsCount, parent);
            
            dungeonInfo.WallsByRoom = wallsPlacer.Place(dungeonInfo.GetAllMarkersByRoom(room => room.AllWallsMarkers));
            dungeonInfo.DoorsByRoom = doorsPlacer.Place(dungeonInfo.GetAllMarkersByRoom(room => room.AllDoorMarkers));
            dungeonInfo.ExitsByRoom = exitsPlacer.Place(dungeonInfo.GetAllMarkersByRoom(room => room.AllExitMarkers));
            
            dungeonInfo.PlayersByRoom =
                playersPlacer.Place(dungeonInfo.GetAllMarkersByRoom(room => room.AllPlayerMarkers));
        }
        #endregion
    }
}