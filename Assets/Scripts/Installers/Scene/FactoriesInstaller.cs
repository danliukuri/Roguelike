using Roguelike.Core.Factories;
using UnityEngine;
using Zenject;

namespace Roguelike.Installers.Scene
{
    public class FactoriesInstaller : MonoInstaller
    {
        #region Fields 
        [SerializeField] GameObject wallFactory;
        [SerializeField] GameObject[] roomsFactories;
        #endregion

        #region Methods
        public override void InstallBindings()
        {
            BindWallFactory();
            foreach (GameObject roomFactory in roomsFactories)
                BindRoomFactory(roomFactory);
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
        void BindRoomFactory(GameObject roomFactory)
        {
            Container
                .Bind<IRoomFactory>()
                .FromComponentInNewPrefab(roomFactory)
                .UnderTransform(transform)
                .AsCached()
                .NonLazy();
        }
        #endregion
    }
}