using Roguelike.Core.Factories;
using UnityEngine;
using Zenject;

namespace Roguelike.Installers.Bootstrap
{
    public class FactoriesInstaller : MonoInstaller
    {
        #region Fields 
        [SerializeField] Object[] roomsFactories;
        [SerializeField] GameObject objectsContainerFactory;
        [Header("Room elements:")]
        [SerializeField] GameObject wallFactory;
        [SerializeField] GameObject playerFactory;
        [SerializeField] GameObject enemyFactory;
        [SerializeField] GameObject keyFactory;
        [SerializeField] GameObject doorFactory;
        [SerializeField] GameObject exitFactory;
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
                .NonLazy();
        }
        void BindRoomElementFactories()
        {
            (Object Prefab, RoomElementType ParentId)[] roomElementFactories =
            {
                (wallFactory, RoomElementType.Wall),
                (playerFactory, RoomElementType.Player),
                (enemyFactory, RoomElementType.Enemy),
                (keyFactory, RoomElementType.Key),
                (doorFactory, RoomElementType.Door),
                (exitFactory, RoomElementType.Exit)
            };
            
            foreach ((Object Prefab, RoomElementType ParentId) factory in roomElementFactories)
            {
                static bool IsParentContextIdEqual<T>(InjectContext context, T id)
                {
                    object parentId = context.ParentContext?.Identifier;
                    return parentId != default && Equals(parentId, id);
                }

                Container
                    .Bind<IGameObjectFactory>()
                    .FromComponentInNewPrefab(factory.Prefab)
                    .UnderTransform(transform)
                    .AsCached()
                    .When(context => IsParentContextIdEqual(context, factory.ParentId))
                    .NonLazy();
            }
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