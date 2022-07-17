using UnityEngine;
using Zenject;

namespace Roguelike.Utilities.Pools
{
    public class PoolableObjectsReturner : IInitializable
    {
        #region Fields
        ObjectsPool[] pools;
        GameObject factories;
        #endregion

        #region Methods
        [Inject]
        void Construct(GameObject factories) => this.factories = factories;
        public void Initialize() => pools = factories.GetComponentsInChildren<ObjectsPool>();
        
        public void ReturnAllToPool()
        {
            for (int i = 0; i < pools.Length; i++)
                pools[i].ReturnToPoolAllObjects();
        }
        #endregion
    }
}