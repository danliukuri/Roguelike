using System;
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
            BindRoomElementsPlacers();
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
        void BindRoomElementsPlacers()
        {
            (object Id, Type TypeToBind)[] roomElementsPlacers =
            {
                (RoomElementType.Player, typeof(PlayersPlacer)),
                (RoomElementType.Wall, typeof(WallsPlacer)),
                (RoomElementType.Door, typeof(DoorsPlacer)),
                (RoomElementType.Exit, typeof(ExitsPlacer))
            };
            
            foreach ((object Id, Type TypeToBind) placer in roomElementsPlacers) 
                Container
                    .Bind<IRoomElementsPlacer>()
                    .WithId(placer.Id)
                    .To(placer.TypeToBind)
                    .AsCached();
        }
        #endregion
    }
}