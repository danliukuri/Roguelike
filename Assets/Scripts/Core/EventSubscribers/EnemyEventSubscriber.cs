using Roguelike.Core.EventHandlers;
using Roguelike.Core.Movers;
using Roguelike.Finishers;
using UnityEngine;
using Zenject;

namespace Roguelike.Core.EventSubscribers
{
    public class EnemyEventSubscriber : MonoBehaviour
    {
        #region Fields
        EnemyEventHandler eventHandler;
        TurnFinisher turnFinisher;
        EntityMover playerMover;
        #endregion
        
        #region Methods
        [Inject]
        void Construct(EnemyEventHandler eventHandler, TurnFinisher turnFinisher, EntityMover playerMover)
        {
            (this.eventHandler = eventHandler).SetTurnFinisher(this.turnFinisher = turnFinisher);
            this.playerMover = playerMover;
        }
        
        void OnEnable()
        {
            playerMover.ActionCompleted += eventHandler.OnPlayerActionCompleted;
            turnFinisher.Finished += eventHandler.OnTurnFinished;
        }
        void OnDisable()
        {
            playerMover.ActionCompleted -= eventHandler.OnPlayerActionCompleted;
            turnFinisher.Finished -= eventHandler.OnTurnFinished;
        }
        #endregion
    }
}