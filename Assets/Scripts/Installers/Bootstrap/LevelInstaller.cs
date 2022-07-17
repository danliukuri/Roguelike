using Roguelike.Core.EventHandlers;
using Roguelike.Core.EventSubscribers;
using UnityEngine;
using Zenject;

namespace Roguelike.Installers.Bootstrap
{
    public class LevelInstaller : MonoInstaller
    {
        #region Fields
        [SerializeField] Vector3 firstRoomPosition;
        [SerializeField] Transform environmentParent;
        [SerializeField] int initialNumberOfRooms;
        [SerializeField] int initialNumberOfKeys;
        #endregion

        #region Methods
        public override void InstallBindings()
        {
            BindLoader();
            BindEventHandler();
            BindEventSubscriber();
        }

        void BindLoader()
        {
            Container
                .BindInterfacesAndSelfTo<LevelLoader>()
                .FromNew()
                .AsSingle()
                .WithArguments(firstRoomPosition, environmentParent, initialNumberOfRooms, initialNumberOfKeys);
        }
        void BindEventHandler()
        {
            Container
                .BindInterfacesAndSelfTo<LevelEventHandler>()
                .FromNew()
                .AsSingle();
        }
        void BindEventSubscriber()
        {
            Container
                .BindInterfacesAndSelfTo<LevelEventSubscriber>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }
        #endregion
    }
}