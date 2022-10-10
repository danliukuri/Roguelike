using Roguelike.Utilities.Pooling;
using UnityEngine;
using Zenject;

namespace Roguelike.Setup.Installation.Bootstrap
{
    public class PoolableObjectsInstaller : MonoInstaller
    {
        #region Fields
        [SerializeField] GameObject factories;
        #endregion
        
        #region Methods
        public override void InstallBindings()
        {
            BindReturner();
        }
        void BindReturner()
        {
            Container
                .BindInterfacesAndSelfTo<PoolableObjectsReturner>()
                .FromNew()
                .AsSingle()
                .WithArguments(factories);
        }
        #endregion
    }
}