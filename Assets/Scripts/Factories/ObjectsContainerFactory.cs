using System.Collections.Generic;
using Roguelike.Core.Factories;
using Roguelike.Utilities;
using Roguelike.Utilities.Pools;
using UnityEngine;

namespace Roguelike.Factories
{
    public class ObjectsContainerFactory : MonoBehaviour, IObjectsContainerFactory
    {
        #region Fields
        [SerializeField] ObjectsPool objectsContainersPool;
        readonly Dictionary<string, GameObject> containers = new Dictionary<string, GameObject>();
        #endregion

        #region Methods
        public GameObject GetContainer(string containerName)
        {
            GameObject container;
            if (containers.ContainsKey(containerName))
                container = containers[containerName];
            else
            {
                container = objectsContainersPool.GetFreeObject();
                container.name = containerName;
                containers.Add(containerName, container);
            }
            return container;
        }
        #endregion
    }
}