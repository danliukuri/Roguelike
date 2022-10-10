using Roguelike.Core.Gameplay.Spawning;
using Roguelike.Core.Information;
using Roguelike.Core.Setup.Factories;
using Roguelike.Setup.Loading;
using Roguelike.Utilities.Extensions.Extenject;
using UnityEngine;
using Zenject;

namespace Roguelike.Setup.Installation.Bootstrap
{
    public class FactoriesInstaller : MonoInstaller
    {
        #region Fields
        [SerializeField] Object[] roomsFactories;
        [Header("Room elements:")]
        [SerializeField] GameObject wallFactory;
        [SerializeField] GameObject playerFactory;
        [SerializeField] GameObject enemyFactory;
        [SerializeField] GameObject keyFactory;
        [SerializeField] GameObject doorFactory;
        [SerializeField] GameObject exitFactory;
        [SerializeField] GameObject enemySalivaFactory;
        [SerializeField] GameObject enemyBigSalivaFactory;
        [SerializeField] GameObject enemyStatusFactory;
        #endregion
        
        #region Methods
        public override void InstallBindings()
        {
            foreach (Object roomFactory in roomsFactories)
                BindRoomFactory(roomFactory);
            
            BindRoomElementFactories();
            BindSalivaFactories();
            BindStatusFactories();
        }
        void BindRoomFactory(Object roomFactory)
        {
            Container
                .Bind<IRoomFactory>()
                .FromComponentInNewPrefab(roomFactory)
                .UnderTransform(transform)
                .AsCached()
                .NonLazy();
        }
        void BindRoomElementFactories()
        {
            (Object Prefab, object ParentId)[] roomElementFactories =
            {
                (wallFactory, RoomElementType.Wall),
                (playerFactory, EntityType.Player),
                (enemyFactory, EntityType.Enemy),
                (keyFactory, RoomElementType.Key),
                (doorFactory, RoomElementType.Door),
                (exitFactory, RoomElementType.Exit)
            };
            
            foreach ((Object Prefab, object ParentId) factory in roomElementFactories)
                Container
                    .Bind<IGameObjectFactory>()
                    .FromComponentInNewPrefab(factory.Prefab)
                    .UnderTransform(transform)
                    .AsCached()
                    .WhenParentContextIdEqual(factory.ParentId)
                    .NonLazy();
        }
        void BindSalivaFactories()
        {
            Container
                .Bind<IGameObjectFactory>()
                .WithId(EntityType.Saliva)
                .FromComponentInNewPrefab(enemySalivaFactory)
                .UnderTransform(transform)
                .AsCached()
                .WhenInjectedInto<EnemySalivator>()
                .NonLazy();
            
            Container
                .Bind<IGameObjectFactory>()
                .WithId(EntityType.BigSaliva)
                .FromComponentInNewPrefab(enemyBigSalivaFactory)
                .UnderTransform(transform)
                .AsCached()
                .WhenInjectedInto<EnemySalivator>()
                .NonLazy();
        }
        void BindStatusFactories()
        {
            Container
                .Bind<IGameObjectFactory>()
                .FromComponentInNewPrefab(enemyStatusFactory)
                .UnderTransform(transform)
                .AsCached()
                .WhenInjectedInto<GameObjectLoader>()
                .NonLazy();
        }
        #endregion
    }
}