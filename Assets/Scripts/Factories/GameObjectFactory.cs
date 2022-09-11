using Roguelike.Core.Configurators;
using Roguelike.Core.Factories;
using Roguelike.Utilities.Pools;
using UnityEngine;

namespace Roguelike.Factories
{
    public class GameObjectFactory : MonoBehaviour, IGameObjectFactory
    {
        #region Fields
        [SerializeField] ObjectsPool objectsPool;
        IConfigurator<GameObject>[] gameObjectConfigurators;
        #endregion
        
        #region Methods
        void Awake() => gameObjectConfigurators = GetComponents<IConfigurator<GameObject>>();
        
        public virtual GameObject GetGameObject()
        {
            GameObject freeGameObject = objectsPool.GetFreeObject();
            foreach (IConfigurator<GameObject> gameObjectConfigurator in gameObjectConfigurators)
                gameObjectConfigurator.Configure(freeGameObject);
            return freeGameObject;
        }
        #endregion
    }
}