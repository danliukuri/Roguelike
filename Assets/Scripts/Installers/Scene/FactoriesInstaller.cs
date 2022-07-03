using Roguelike.Core.Factories;
using UnityEngine;
using Zenject;

namespace Roguelike.Installers.Scene
{
    public class FactoriesInstaller : MonoInstaller
    {
        #region Fields 
        [SerializeField] GameObject[] roomsFactories;
        [SerializeField] GameObject wallFactory;
        [SerializeField] GameObject doorFactory;
        [SerializeField] GameObject exitFactory;
        [SerializeField] GameObject playerFactory;
        #endregion

        #region Methods
        public override void InstallBindings()
        {
            foreach (GameObject roomFactory in roomsFactories)
                BindRoomFactory(roomFactory);
            BindWallFactory();
            BindDoorFactory();
            BindExitFactory();
            BindPlayerFactory();
        }
        void BindRoomFactory(GameObject roomFactory)
        {
            Container
                .Bind<IRoomFactory>()
                .FromComponentInNewPrefab(roomFactory)
                .UnderTransform(transform)
                .AsCached()
                .NonLazy();
        }
        void BindWallFactory()
        {
            Container
                .Bind<IWallFactory>()
                .FromComponentInNewPrefab(wallFactory)
                .UnderTransform(transform)
                .AsSingle()
                .NonLazy();
        }
        void BindDoorFactory()
        {
            Container
                .Bind<IDoorFactory>()
                .FromComponentInNewPrefab(doorFactory)
                .UnderTransform(transform)
                .AsSingle()
                .NonLazy();
        }
        void BindExitFactory()
        {
            Container
                .Bind<IExitFactory>()
                .FromComponentInNewPrefab(exitFactory)
                .UnderTransform(transform)
                .AsSingle()
                .NonLazy();
        }
        void BindPlayerFactory()
        {
            Container
                .Bind<IPlayerFactory>()
                .FromComponentInNewPrefab(playerFactory)
                .UnderTransform(transform)
                .AsSingle()
                .NonLazy();
        }
        #endregion
    }
}