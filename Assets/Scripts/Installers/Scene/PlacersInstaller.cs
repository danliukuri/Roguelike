using Roguelike.Core.Placers;
using Roguelike.Placers;
using Zenject;

namespace Roguelike.Installers.Scene
{
    public class PlacersInstaller : MonoInstaller
    {
        #region Methods
        public override void InstallBindings()
        {
            BindDungeonPlacer();
            BindRoomsPlacer();
            BindWallsPlacer();
            BindDoorsPlacer();
            BindExitsPlacer();
            BindPlayersPlacer();
        }
        void BindDungeonPlacer()
        {
            Container
                .Bind<IDungeonPlacer>()
                .To<DungeonPlacer>()
                .AsSingle();
        }
        void BindRoomsPlacer()
        {
            Container
                .Bind<IRoomsPlacer>()
                .To<RoomsPlacer>()
                .AsSingle();
        }
        void BindWallsPlacer()
        {
            Container
                .Bind<IWallsPlacer>()
                .To<WallsPlacer>()
                .AsSingle();
        }
        void BindDoorsPlacer()
        {
            Container
                .Bind<IDoorsPlacer>()
                .To<DoorsPlacer>()
                .AsSingle();
        }
        void BindExitsPlacer()
        {
            Container
                .Bind<IExitsPlacer>()
                .To<ExitsPlacer>()
                .AsSingle();
        }
        void BindPlayersPlacer()
        {
            Container
                .Bind<IPlayersPlacer>()
                .To<PlayersPlacer>()
                .AsSingle();
        }
        #endregion
    }
}