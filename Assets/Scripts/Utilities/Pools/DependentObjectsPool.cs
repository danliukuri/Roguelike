using UnityEngine;
using Zenject;

namespace Roguelike.Utilities.Pools
{
    public class DependentObjectsPool : ObjectsPool
    {
        #region Fields
        DiContainer container;
        #endregion

        #region Methods
        [Inject]
        public void Construct(DiContainer container) => this.container = container;
        
        /// <summary>
        /// Creates a new free inactive object
        /// </summary>
        /// <returns> Newly created inactive object </returns>
        protected override GameObject CreateInactiveGameObject()
        {
            GameObject inactiveGameObject = container.InstantiatePrefab(gameObjectPrefab, objectsParent);
            inactiveGameObject.SetActive(false);
            objects.Add(inactiveGameObject);
            return inactiveGameObject;
        }   
        #endregion
    }
}