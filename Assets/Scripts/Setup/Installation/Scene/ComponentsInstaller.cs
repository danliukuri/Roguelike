using Roguelike.Core.Characteristics;
using Roguelike.Utilities.Extensions.Extenject;
using UnityEngine;
using Zenject;

namespace Roguelike.Setup.Installation.Scene
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
                .FromMultipleUntyped(resettableComponents)
                .AsCached();
        }
        #endregion
    }
}