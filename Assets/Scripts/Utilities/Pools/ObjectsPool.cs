using System.Collections.Generic;
using System.Linq;
using Roguelike.Core;
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
        
        List<GameObject> objects;
        #endregion

        #region Methods
        void Awake()
        {
            Initialize();
        }
        void Initialize()
        {
            objects = new List<GameObject>(initialNumberOfObjects);
            for (int i = 0; i < initialNumberOfObjects; i++)
                CreateInactiveGameObject();
        }

        /// <summary>
        /// Creates a new free inactive object
        /// </summary>
        /// <returns> Newly created inactive object </returns>
        GameObject CreateInactiveGameObject()
        {
            GameObject inactiveGameObject = InstantiatePrefab();
            inactiveGameObject.SetActive(false);
            objects.Add(inactiveGameObject);
            return inactiveGameObject;
        }
        protected virtual GameObject InstantiatePrefab() => Instantiate(gameObjectPrefab, objectsParent);

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

        public void ReturnToPoolAllObjects()
        {
            foreach (GameObject obj in objects)
                ReturnToPool(obj);
        }
        void ReturnToPool(GameObject obj)
        {
            Transform objectTransform = obj.transform;
            if(objectTransform.parent != objectsParent)
                objectTransform.SetParent(objectsParent);
            
            if (obj.activeSelf)
                obj.SetActive(false);
        }
        #endregion
        
#if UNITY_EDITOR
        #region Methods
        void OnValidate()
        {
            if (objectsParent == null)
                objectsParent = transform;
        }
        #endregion
#endif
    }
}