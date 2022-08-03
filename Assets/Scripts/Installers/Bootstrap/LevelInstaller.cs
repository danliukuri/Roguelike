using Roguelike.Core.EventHandlers;
using Roguelike.Core.EventSubscribers;
using Roguelike.Core.Information;
using UnityEngine;
using Zenject;

namespace Roguelike.Installers.Bootstrap
{
    public class LevelInstaller : MonoInstaller
    {
        #region Fields
        [SerializeField] LevelSettings levelSettings;
        #endregion

        #region Methods
        public override void InstallBindings()
        {
            ConfigureRoomElementMarkersCountsPerLevel();
            
            BindLoader();
            BindEventHandler();
            BindEventSubscriber();
        }
        
        void ConfigureRoomElementMarkersCountsPerLevel()
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
                new RoomElementMarkersInfo { GetActualCount = room => room.AllDoorMarkers.Length,
                    RequiredCount = levelSettings.NumberOfKeys },
                new RoomElementMarkersInfo { GetActualCount = room => room.AllExitMarkers.Length,
                    RequiredCount = exitMarkersRequiredCount, RelatedRoomsMaxCount = exitRoomsMaxCount },
                new RoomElementMarkersInfo { GetActualCount = room => room.AllWallsMarkers.Count }
            };
        }
        
        void BindLoader()
        {
            Container
                .BindInterfacesAndSelfTo<LevelLoader>()
                .FromNew()
                .AsSingle()
                .WithArguments(levelSettings);
        }
        void BindEventHandler()
        {
            Container
                .BindInterfacesAndSelfTo<LevelEventHandler>()
                .FromNew()
                .AsSingle();
        }
        void BindEventSubscriber()
        {
            Container
                .BindInterfacesAndSelfTo<LevelEventSubscriber>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }
        #endregion
    }
}