using System;
using Roguelike.Core.Placers;
using Roguelike.Placers;
using UnityEngine;
using Zenject;

namespace Roguelike.Installers.Scene
{
    public class PlacersInstaller : MonoInstaller
    {
        #region Fields
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
                .AsSingle();
        }
        void BindRoomElementsPlacers()
        {
            (object Id, Type TypeToBind, Transform ElementsParent)[] roomElementsPlacers =
            {
                (RoomElementType.Wall, typeof(RoomElementsPlacer), default),
                (RoomElementType.Player, typeof(RoomElementsPlacer), playersParent),
                (RoomElementType.Enemy, typeof(RoomElementsPlacer), enemiesParent),
                (RoomElementType.Key, typeof(RoomElementsPlacer), default),
                (RoomElementType.Door, typeof(RoomElementsPlacer), default),
                (RoomElementType.Exit, typeof(RoomElementsPlacer), default)
            };
            
            foreach ((object Id, Type TypeToBind, Transform ElementsParent) placer in roomElementsPlacers)
                Container
                    .Bind<IRoomElementsPlacer>()
                    .WithId(placer.Id)
                    .To(placer.TypeToBind)
                    .AsCached()
                    .WithArguments(placer.ElementsParent);
        }
        #endregion
    }
}