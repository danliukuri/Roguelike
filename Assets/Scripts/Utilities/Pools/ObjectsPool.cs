using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Roguelike.Utilities.Pools
{
    public class ObjectsPool : MonoBehaviour
    {
        #region Properies
        public Transform ObjectsParent => objectsParent;
        #endregion

        #region Fields
        [SerializeField] protected GameObject gameObjectPrefab;
        [SerializeField] protected Transform objectsParent;

        [SerializeField] int initialNumberOfObjects;
        
        protected List<GameObject> objects;
        #endregion

        #region Methods
        void Awake()
        {
            Initialize();
        }
        protected void Initialize()
        {
            objects = new List<GameObject>(initialNumberOfObjects);
            for (int i = 0; i < initialNumberOfObjects; i++)
                CreateInactiveGameObject();
        }

        /// <summary>
        /// Creates a new free inactive object
        /// </summary>
        /// <returns> Newly created inactive object </returns>
        protected virtual GameObject CreateInactiveGameObject()
        {
            GameObject inactiveGameObject = Instantiate(gameObjectPrefab, objectsParent);
            inactiveGameObject.SetActive(false);
            objects.Add(inactiveGameObject);
            return inactiveGameObject;
        }
        protected void OnValidate()
        {
            if (objectsParent == null)
                objectsParent = transform;
        }

        /// <summary>
        /// Finds an inactive object or creates a new one
        /// </summary>
        /// <returns> Found or newly created inactive object </returns>
        public GameObject GetFreeObject()
        {
            GameObject freeGameObject = objects.FirstOrDefault(obj => !obj.activeSelf);
            if (freeGameObject == default)
                freeGameObject = CreateInactiveGameObject();
            return freeGameObject;
        }
        #endregion
    }
}