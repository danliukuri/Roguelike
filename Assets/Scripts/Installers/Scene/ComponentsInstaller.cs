using Roguelike.Core;
using UnityEngine;
using Zenject;

namespace Roguelike.Installers.Scene
{
    public class ComponentsInstaller : MonoInstaller
    {
        #region Fields
        [SerializeField] Component[] resettableComponents;
        #endregion
        
        #region Methods
        public override void InstallBindings()
        {
            BindResettableComponents();
        }
        void BindResettableComponents()
        {
            Container
                .Bind<IResettable>()
                .FromMethodMultipleUntyped(context => resettableComponents)
                .AsCached();
        }
        #endregion
    }
}