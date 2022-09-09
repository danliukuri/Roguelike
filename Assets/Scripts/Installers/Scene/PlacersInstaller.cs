using Roguelike.Core.Information;
using Roguelike.Core.Placers;
using Roguelike.Placers;
using UnityEngine;
using Zenject;

namespace Roguelike.Installers.Scene
{
    public class PlacersInstaller : MonoInstaller
    {
        #region Fields
        [SerializeField] Transform roomsParent;
        [SerializeField] Transform playersParent;
        [SerializeField] Transform enemiesParent;
        #endregion

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
                .AsSingle()
                .WithArguments(roomsParent);
        }
        void BindRoomElementsPlacers()
        {
            (object Id, Transform ElementsParent)[] roomElementsPlacers =
            {
                (RoomElementType.Wall, default),
                (EntityType.Player, playersParent),
                (EntityType.Enemy, enemiesParent),
                (RoomElementType.Key, default),
                (RoomElementType.Door, default),
                (RoomElementType.Exit, default)
            };
            
            foreach ((object Id, Transform ElementsParent) placer in roomElementsPlacers)
                Container
                    .Bind<IRoomElementsPlacer>()
                    .WithId(placer.Id)
                    .To<RoomElementsPlacer>()
                    .AsCached()
                    .WithArguments(placer.ElementsParent);
        }
        #endregion
    }
}