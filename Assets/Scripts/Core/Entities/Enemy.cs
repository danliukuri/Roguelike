using Roguelike.Core.Information;
using Roguelike.Core.Movers;
using UnityEngine;
using Zenject;

namespace Roguelike.Core.Entities
{
    public class Enemy : MonoBehaviour
    {
        #region Fields
        private EnemiesInfo enemiesInfo;
        private EntityMover mover;
        #endregion
        
        #region Methods
        [Inject]
        void Construct(EnemiesInfo enemiesInfo)
        {
            this.enemiesInfo = enemiesInfo;
            mover = GetComponent<EntityMover>();
        }
        void OnEnable() => enemiesInfo.AddEnemy(mover);
        void OnDisable() => enemiesInfo.RemoveEnemy(mover);
        #endregion
    }
}