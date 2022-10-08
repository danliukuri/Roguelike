using Roguelike.Core.Information;

namespace Roguelike
{
    public class LevelSettingsUpdater
    {
        #region Fields
        readonly LevelSettings levelSettings;
        #endregion
        
        #region Methods
        public LevelSettingsUpdater(LevelSettings levelSettings) => this.levelSettings = levelSettings.Initialize();
        
        public void Update()
        {
            levelSettings.NumberOfRooms += levelSettings.LevelNumber;
            levelSettings.NumberOfEnemies += levelSettings.LevelNumber;
            ConfigureRoomElementMarkersCountsPerLevel();
            levelSettings.LevelNumber++;
        }
        public void ConfigureRoomElementMarkersCountsPerLevel()
        {
            const int playerMarkersRequiredCount = 1, playerRoomsMaxCount = 1;
            const int exitMarkersRequiredCount = 1, exitRoomsMaxCount = 1;
            levelSettings.RoomElementMarkersInfo = new[]
            {
                new RoomElementMarkersInfo { GetActualCount = room => room.AllPlayerMarkers.Length,
                    RequiredCount = playerMarkersRequiredCount, RelatedRoomsMaxCount = playerRoomsMaxCount },
                new RoomElementMarkersInfo { GetActualCount = room => room.AllItemMarkers.Length,
                    RequiredCount = levelSettings.NumberOfKeys},
                new RoomElementMarkersInfo { GetActualCount = room => room.AllEnemyMarkers.Length,
                    RequiredCount = levelSettings.NumberOfEnemies},
                new RoomElementMarkersInfo { GetActualCount = room => room.AllExitMarkers.Length,
                    RequiredCount = exitMarkersRequiredCount, RelatedRoomsMaxCount = exitRoomsMaxCount }
            };
        }
        #endregion
    }
}