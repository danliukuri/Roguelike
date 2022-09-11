using Roguelike.Core.Factories;
using Roguelike.Utilities.Pools;
using UnityEngine;

namespace Roguelike.Factories
{
    public class GameObjectFactory : MonoBehaviour, IGameObjectFactory
    {
        #region Fields
        [SerializeField] ObjectsPool objectsPool;
        #endregion
        
        #region Methods
        public virtual GameObject GetGameObject() => objectsPool.GetFreeObject();
        #endregion
    }
}