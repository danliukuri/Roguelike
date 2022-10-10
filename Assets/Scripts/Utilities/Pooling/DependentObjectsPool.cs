using UnityEngine;
using Zenject;

namespace Roguelike.Utilities.Pooling
{
    public class DependentObjectsPool : ObjectsPool
    {
        #region Fields
        DiContainer container;
        #endregion
        
        #region Methods
        [Inject]
        public void Construct(DiContainer container) => this.container = container;
        
        protected override GameObject InstantiatePrefab() =>
            container.InstantiatePrefab(gameObjectPrefab, objectsParent);
        #endregion
    }
}