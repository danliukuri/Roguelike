using Roguelike.Utilities.Pools;
using UnityEngine;
using Zenject;

namespace Roguelike.Installers.Bootstrap
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