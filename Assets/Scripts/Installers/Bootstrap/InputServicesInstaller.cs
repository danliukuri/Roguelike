using Roguelike.Services.Input;
using UnityEngine;
using Zenject;

namespace Roguelike.Installers.Bootstrap
{
    public class InputServicesInstaller : MonoInstaller
    {
        #region Fields
        [SerializeField] float timeToHoldMoveKeyBeforeMoving;
        #endregion

        #region Methods
        public override void InstallBindings()
        {
            BindMovementInputService();
        }

        void BindMovementInputService()
        {
            Container
                .BindInterfacesTo<StandaloneUserMovementInput>()
                .FromNew()
                .AsSingle()
                .WithArguments(timeToHoldMoveKeyBeforeMoving);
        }
        #endregion
    }
}