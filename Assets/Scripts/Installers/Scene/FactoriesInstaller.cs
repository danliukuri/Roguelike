using Roguelike.Core.Factories;
using Roguelike.Core.Placers;
using UnityEngine;
using Zenject;

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
            (object Id, Object Prefab)[] roomElementFactories =
            {
                (RoomElementType.Player, playerFactory),
                (RoomElementType.Wall, wallFactory),
                (RoomElementType.Door, doorFactory),
                (RoomElementType.Exit, exitFactory)
            };
            
            foreach ((object Id, Object Prefab) factory in roomElementFactories) 
                Container
                    .Bind<IGameObjectFactory>()
                    .WithId(factory.Id)
                    .FromComponentInNewPrefab(factory.Prefab)
                    .UnderTransform(transform)
                    .AsCached()
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