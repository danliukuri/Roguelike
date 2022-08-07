using Roguelike.Core.EventHandlers;
using Roguelike.Core.Movers;
using UnityEngine;
using Zenject;

namespace Roguelike.Core.EventSubscribers
{
    public class EnemyEventSubscriber : MonoBehaviour
    {
        #region Fields
        EnemyEventHandler eventHandler;
        EntityMover playerMover;
        #endregion
        
        #region Methods
        [Inject]
        void Construct(EnemyEventHandler eventHandler, EntityMover playerMover)
        {
            this.eventHandler = eventHandler;
            this.playerMover = playerMover;
        }
        
        void OnEnable() => playerMover.ActionCompleted += eventHandler.OnPlayerActionCompleted;
        void OnDisable() => playerMover.ActionCompleted -= eventHandler.OnPlayerActionCompleted;
        #endregion
    }
}