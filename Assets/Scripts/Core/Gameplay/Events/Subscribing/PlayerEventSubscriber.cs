using Roguelike.Core.Gameplay.Events.Handling;
using Roguelike.Core.Gameplay.Services.Input;
using Roguelike.Core.Gameplay.Transformation.Moving;
using Roguelike.Core.Information;
using UnityEngine;
using Zenject;

namespace Roguelike.Core.Gameplay.Events.Subscribing
{
    public class PlayerEventSubscriber : MonoBehaviour
    {
        #region Fields
        IMovementInputService movementInputService;
        EntityMover mover;
        EnemiesInfo enemiesInfo;
        
        PlayerEventHandler eventHandler;
        #endregion
        
        #region Methods
        [Inject]
        void Construct(IMovementInputService movementInputService, EntityMover mover, PlayerEventHandler eventHandler,
            EnemiesInfo enemiesInfo)
        {
            this.movementInputService = movementInputService;
            this.mover = mover;
            (this.eventHandler = eventHandler).SetSubscriber(this);
            this.enemiesInfo = enemiesInfo;
        }
        
        void OnEnable()
        {
            movementInputService.Moving += eventHandler.OnInputServiceMoving;
            mover.Moving += eventHandler.OnMoving;
            mover.MovingToKey += eventHandler.OnMovingToKey;
            mover.MovingToDoor += eventHandler.OnMovingToDoor;
            mover.MovingToEnemy += eventHandler.OnPlayerDeath;
            enemiesInfo.MoversCountIncreased += SubscribeToEnemyOnMovingToPlayerEvent;
            enemiesInfo.MoversCountDecreased += UnsubscribeFromEnemyOnMovingToPlayerEvent;
        }
        void OnDisable()
        {
            UnsubscribeFromInputServiceMovingEvent();
            mover.Moving -= eventHandler.OnMoving;
            mover.MovingToKey -= eventHandler.OnMovingToKey;
            mover.MovingToDoor -= eventHandler.OnMovingToDoor;
            mover.MovingToEnemy -= eventHandler.OnPlayerDeath;
            enemiesInfo.MoversCountIncreased -= SubscribeToEnemyOnMovingToPlayerEvent;
            enemiesInfo.MoversCountDecreased -= UnsubscribeFromEnemyOnMovingToPlayerEvent;
        }
        
        public void UnsubscribeFromInputServiceMovingEvent() =>
            movementInputService.Moving -= eventHandler.OnInputServiceMoving;
        void SubscribeToEnemyOnMovingToPlayerEvent(EntityMover enemyMover) =>
            enemyMover.MovingToPlayer += eventHandler.OnPlayerDeath;
        void UnsubscribeFromEnemyOnMovingToPlayerEvent(EntityMover enemyMover) =>
            enemyMover.MovingToPlayer -= eventHandler.OnPlayerDeath;
        #endregion
    }
}