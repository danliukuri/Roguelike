using Roguelike.Core.EventHandlers;
using Roguelike.Core.Rotators;
using Roguelike.Core.Services.Trackers;
using UnityEngine;
using Zenject;

namespace Roguelike.Core.EventSubscribers
{
    public class TargetDetectionStatusEventSubscriber : MonoBehaviour
    {
        #region Fields
        Transform parent;
        SpriteRotator parentRotator;
        TargetMovingTracker parentTargetMovingTracker;
        TargetDetectionStatusEventHandler eventHandler;
        #endregion
        
        #region Methods
        [Inject]
        void Construct(TargetDetectionStatusEventHandler eventHandler) => this.eventHandler = eventHandler;
        
        void OnEnable()
        {
            parent = transform.parent;
            SubscribeOnParentTargetDetectedAndOverlookedEvents();
            SubscribeOnParentRotatorEvents();
            if (parentRotator != default)
                eventHandler.OnEnable(parentRotator.IsLeftRotated());
        }
        void OnDisable()
        {
            UnsubscribeFromParentTargetDetectedAndOverlookedEvents();
            UnsubscribeFromParentRotatorEvents();
        }
        
        void SubscribeOnParentTargetDetectedAndOverlookedEvents()
        {
            if (parentTargetMovingTracker == default)
                if (parent.TryGetComponent(out EnemyEventSubscriber enemyEventSubscriber))
                    parentTargetMovingTracker = enemyEventSubscriber.TargetMovingTracker;
            if (parentTargetMovingTracker != default)
            {
                parentTargetMovingTracker.TargetDetected += eventHandler.OnParentTargetDetected;
                parentTargetMovingTracker.TargetOverlooked += eventHandler.OnParentTargetOverlooked;
            }
        }
        void SubscribeOnParentRotatorEvents()
        {
            if (parentRotator != default || parent.TryGetComponent(out parentRotator))
            {
                parentRotator.RotatedLeft += eventHandler.OnParentRotatedLeft;
                parentRotator.RotatedRight += eventHandler.OnParentRotatedRight;
            }
        }
        
        void UnsubscribeFromParentTargetDetectedAndOverlookedEvents()
        {
            if (parentTargetMovingTracker != default)
            {
                parentTargetMovingTracker.TargetDetected -= eventHandler.OnParentTargetDetected;
                parentTargetMovingTracker.TargetOverlooked -= eventHandler.OnParentTargetOverlooked;
            }
        }
        void UnsubscribeFromParentRotatorEvents()
        {
            if (parentRotator != default)
            {
                parentRotator.RotatedLeft -= eventHandler.OnParentRotatedLeft;
                parentRotator.RotatedRight -= eventHandler.OnParentRotatedRight;
            }
        }
        #endregion
    }
}