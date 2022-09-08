using Roguelike.Core.EventHandlers;
using Roguelike.Core.Movers;
using Roguelike.Core.Services.Trackers;
using Roguelike.Finishers;
using UnityEngine;
using Zenject;

namespace Roguelike.Core.EventSubscribers
{
    [RequireComponent(typeof(EntityMover))]
    public class EnemyEventSubscriber : MonoBehaviour, IResettable
    {
        #region Properties
        public TargetMovingTracker TargetMovingTracker { get; private set; }
        #endregion
        
        #region Fields
        EnemyEventHandler eventHandler;
        TurnFinisher turnFinisher;
        EntityMover playerMover;
        EntityMover mover;
        #endregion
        
        #region Methods
        [Inject]
        void Construct(EnemyEventHandler eventHandler, TurnFinisher turnFinisher, EntityMover playerMover,
            TargetMovingTracker targetMovingTracker)
        {
            this.eventHandler = eventHandler;
            eventHandler.SetTurnFinisher(this.turnFinisher = turnFinisher);
            eventHandler.SetTargetMovingTracker(TargetMovingTracker = targetMovingTracker);
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