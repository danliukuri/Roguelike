using System;
using System.Collections.Generic;
using Roguelike.Core.Movers;

namespace Roguelike.Core.Information
{
    public class EnemiesInfo
    {
        #region Fields
        public event Action<EntityMover> MoversCountIncreased;
        public event Action<EntityMover> MoversCountDecreased;
        List<EntityMover> movers = new List<EntityMover>();
        #endregion

        #region Methods
        public void AddEnemy(EntityMover mover) => AddEnemyMover(mover);
        public void RemoveEnemy(EntityMover mover) => RemoveEnemyMover(mover);

        void AddEnemyMover(EntityMover mover)
        {
            movers.Add(mover);
            MoversCountIncreased?.Invoke(mover);
        }
        void RemoveEnemyMover(EntityMover mover)
        {
            movers.Remove(mover);
            MoversCountDecreased?.Invoke(mover);
        }
        #endregion
    }
}