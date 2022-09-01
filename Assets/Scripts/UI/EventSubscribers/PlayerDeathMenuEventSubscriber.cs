using System;
using Roguelike.Core.Information;
using Roguelike.Core.Movers;
using Roguelike.UI.EventHandlers;
using Zenject;

namespace Roguelike.UI.EventSubscribers
{
    public class PlayerDeathMenuEventSubscriber : IInitializable, IDisposable
    {
        #region Fields
        readonly EntityMover playerMover;
        readonly EnemiesInfo enemiesInfo;
        
        readonly PlayerDeathMenuEventHandler eventHandler;
        #endregion
        
        #region Methods
        public PlayerDeathMenuEventSubscriber(PlayerDeathMenuEventHandler eventHandler, EntityMover playerMover,
            EnemiesInfo enemiesInfo)
        {
            this.eventHandler = eventHandler;
            this.playerMover = playerMover;
            this.enemiesInfo = enemiesInfo;
        }
        
        public void Initialize()
        {
            playerMover.MovingToEnemy += eventHandler.OnPlayerDeath;
            enemiesInfo.MoversCountIncreased += SubscribeToEnemyOnMovingToPlayerEvent;
            enemiesInfo.MoversCountDecreased += UnsubscribeFromEnemyOnMovingToPlayerEvent;
        }
        public void Dispose()
        {
            playerMover.MovingToEnemy -= eventHandler.OnPlayerDeath;
            enemiesInfo.MoversCountIncreased -= SubscribeToEnemyOnMovingToPlayerEvent;
            enemiesInfo.MoversCountDecreased -= UnsubscribeFromEnemyOnMovingToPlayerEvent;
        }
        
        void SubscribeToEnemyOnMovingToPlayerEvent(EntityMover enemyMover) =>
            enemyMover.MovingToPlayer += eventHandler.OnPlayerDeath;
        void UnsubscribeFromEnemyOnMovingToPlayerEvent(EntityMover enemyMover) =>
            enemyMover.MovingToPlayer -= eventHandler.OnPlayerDeath;
        #endregion
    }
}