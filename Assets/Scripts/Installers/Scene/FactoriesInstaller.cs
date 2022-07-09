using System;
using Roguelike.Core.Factories;
using Roguelike.Core.Placers;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Roguelike.Installers.Scene
{
    public class FactoriesInstaller : MonoInstaller
    {
        #region Fields 
        [SerializeField] Object[] roomsFactories;
        
        [SerializeField] GameObject playerFactory;
        [SerializeField] GameObject wallFactory;
        [SerializeField] GameObject doorFactory;
        [SerializeField] GameObject exitFactory;
        
        [SerializeField] GameObject objectsContainerFactory;
        #endregion

        #region Methods
        public override void InstallBindings()
        {
            foreach (Object roomFactory in roomsFactories)
                BindRoomFactory(roomFactory);
            
            BindRoomElementFactories();
            BindObjectsContainerFactory();
        }

        void BindRoomFactory(Object roomFactory)
        {
            Container
                .Bind<IGameObjectFactory>()
                .FromComponentInNewPrefab(roomFactory)
                .UnderTransform(transform)
                .AsCached()
                .WhenInjectedInto<IRoomsPlacer>()
                .NonLazy();
        }
        void BindRoomElementFactories()
        {
            (Object factoryPrefab, Type typeWhenInjectInto)[] roomElementFactories =
            {
                (playerFactory, typeof(IPlayersPlacer)),
                (wallFactory, typeof(IWallsPlacer)),
                (doorFactory, typeof(IDoorsPlacer)),
                (exitFactory, typeof(IExitsPlacer))
            };
            
            foreach ((Object factoryPrefab, Type target) in roomElementFactories) 
                Container
                    .Bind<IGameObjectFactory>()
                    .FromComponentInNewPrefab(factoryPrefab)
                    .UnderTransform(transform)
                    .AsCached()
                    .WhenInjectedInto(target)
                    .NonLazy();
        }
        void BindObjectsContainerFactory()
        {
            Container
                .Bind<IObjectsContainerFactory>()
                .FromComponentInNewPrefab(objectsContainerFactory)
                .UnderTransform(transform)
                .AsSingle()
                .NonLazy();
        }
        #endregion
    }
}