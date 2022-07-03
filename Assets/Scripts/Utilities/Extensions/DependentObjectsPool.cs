using UnityEngine;
using Zenject;

namespace Roguelike.Utilities.Extensions
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
        public override GameObject CreateInactiveGameObject()
        {
            GameObject gameObject = container.InstantiatePrefab(gameObjectPrefab, objectsParent);
            gameObject.SetActive(false);
            objects.Add(gameObject);
            return gameObject;
        }   
        #endregion
    }
}