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
        [SerializeField] string playersContainerName;
        [SerializeField] string enemiesContainerName;
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
            (object Id, Type TypeToBind, string ContainerName)[] roomElementsPlacers =
            {
                (RoomElementType.Wall, typeof(RoomElementsPlacer), default),
                (RoomElementType.Player, typeof(RoomElementsUnderContainerPlacer), playersContainerName),
                (RoomElementType.Enemy, typeof(RoomElementsUnderContainerPlacer), enemiesContainerName),
                (RoomElementType.Key, typeof(RoomElementsPlacer), default),
                (RoomElementType.Door, typeof(RoomElementsPlacer), default),
                (RoomElementType.Exit, typeof(RoomElementsPlacer), default)
            };
            
            foreach ((object Id, Type TypeToBind, string ContainerName) placer in roomElementsPlacers)
            {
                ConcreteIdArgConditionCopyNonLazyBinder roomElementPlacer = Container
                    .Bind<IRoomElementsPlacer>()
                    .WithId(placer.Id)
                    .To(placer.TypeToBind)
                    .AsCached();

                if (placer.ContainerName != default)
                    roomElementPlacer.WithArguments(placer.ContainerName);
            }
        }
        #endregion
    }
}