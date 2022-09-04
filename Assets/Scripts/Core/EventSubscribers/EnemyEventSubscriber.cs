using Roguelike.Core.EventHandlers;
using Roguelike.Core.Movers;
using Roguelike.Finishers;
using UnityEngine;
using Zenject;

namespace Roguelike.Core.EventSubscribers
{
    [RequireComponent(typeof(EntityMover))]
    public class EnemyEventSubscriber : MonoBehaviour, IResettable
    {
        #region Fields
        EnemyEventHandler eventHandler;
        TurnFinisher turnFinisher;
        EntityMover playerMover;
        EntityMover mover;
        #endregion
        
        #region Methods
        [Inject]
        void Construct(EnemyEventHandler eventHandler, TurnFinisher turnFinisher, EntityMover playerMover)
        {
            (this.eventHandler = eventHandler).SetTurnFinisher(this.turnFinisher = turnFinisher);
            this.playerMover = playerMover;
            mover = GetComponent<EntityMover>();
        }
        public void Reset() => eventHandler.Reset();
        
        void OnEnable()
        {
            mover.Moving += eventHandler.OnMoving;
            playerMover.ActionCompleted += eventHandler.OnPlayerActionCompleted;
            turnFinisher.Finished += eventHandler.OnTurnFinished;
        }
        void OnDisable()
        {
            mover.Moving -= eventHandler.OnMoving;
            playerMover.ActionCompleted -= eventHandler.OnPlayerActionCompleted;
            turnFinisher.Finished -= eventHandler.OnTurnFinished;
        }
        #endregion
    }
}