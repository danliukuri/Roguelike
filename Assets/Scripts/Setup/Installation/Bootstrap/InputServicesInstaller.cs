using Roguelike.Gameplay.Services.Input;
using UnityEngine;
using Zenject;

namespace Roguelike.Setup.Installation.Bootstrap
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
#if UNITY_ANDROID || UNITY_IOS
            Container
                .BindInterfacesTo<PhoneUserMovementInput>()
                .FromNew()
                .AsSingle()
                .WithArguments(timeToHoldMoveKeyBeforeMoving);
#elif UNITY_STANDALONE || UNITY_WEBGL
            Container
                .BindInterfacesTo<StandaloneUserMovementInput>()
                .FromNew()
                .AsSingle()
                .WithArguments(timeToHoldMoveKeyBeforeMoving);
#endif
        }
        #endregion
    }
}