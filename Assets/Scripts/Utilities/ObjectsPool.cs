using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Utilities
{
    public class ObjectsPool : MonoBehaviour
    {
        #region Properies
        public Transform ObjectsParent => objectsParent;
        #endregion

        #region Fields
        [SerializeField] GameObject gameObjectPrefab;
        [SerializeField] Transform objectsParent;

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
        public GameObject CreateInactiveGameObject()
        {
            GameObject gameObject = Instantiate(gameObjectPrefab, objectsParent);
            gameObject.SetActive(false);
            objects.Add(gameObject);
            return gameObject;
        }
        void OnValidate()
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
            GameObject gameObject = TryToGetFreeObject();
            if (gameObject == null)
                gameObject = CreateInactiveGameObject();
            return gameObject;
        }
        /// <summary>
        /// Finds an inactive object
        /// </summary>
        /// <returns> Found inactive object </returns>
        GameObject TryToGetFreeObject()
        {
            GameObject gameObject = null;
            for (int i = 0; i < objects?.Count; i++)
                if (!objects[i].activeSelf)
                {
                    gameObject = objects[i];
                    break;
                }
            return gameObject;
        }
        #endregion
    }
}