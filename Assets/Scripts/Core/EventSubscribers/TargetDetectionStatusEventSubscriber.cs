using Roguelike.Core.EventHandlers;
using Roguelike.Core.Services.Trackers;
using UnityEngine;
using Zenject;

namespace Roguelike.Core.EventSubscribers
{
    public class TargetDetectionStatusEventSubscriber : MonoBehaviour
    {
        #region Fields
        TargetMovingTracker parentTargetMovingTracker;
        TargetDetectionStatusEventHandler eventHandler;
        #endregion
        
        #region Methods
        [Inject]
        void Construct(TargetDetectionStatusEventHandler eventHandler) => this.eventHandler = eventHandler;
        
        void OnEnable()
        {
            if (parentTargetMovingTracker == default)
                if (transform.parent.TryGetComponent(out EnemyEventSubscriber enemyEventSubscriber))
                    parentTargetMovingTracker = enemyEventSubscriber.TargetMovingTracker;
            if (parentTargetMovingTracker != default)
            {
                parentTargetMovingTracker.TargetDetected += eventHandler.OnParentTargetDetected;
                parentTargetMovingTracker.TargetOverlooked += eventHandler.OnParentTargetOverlooked;
            }
            
        }
        void OnDisable()
        {
            if (parentTargetMovingTracker != default)
            {
                parentTargetMovingTracker.TargetDetected -= eventHandler.OnParentTargetDetected;
                parentTargetMovingTracker.TargetOverlooked -= eventHandler.OnParentTargetOverlooked;
            }
        }
        #endregion
    }
}