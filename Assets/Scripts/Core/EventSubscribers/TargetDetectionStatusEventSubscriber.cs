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
        SpriteRotator parentRotator;
        TargetMovingTracker parentTargetMovingTracker;
        TargetDetectionStatusEventHandler eventHandler;
        #endregion
        
        #region Methods
        [Inject]
        void Construct(TargetDetectionStatusEventHandler eventHandler) => this.eventHandler = eventHandler;
        
        void OnEnable()
        {
            Transform parent = transform.parent;
            SubscribeOnParentTargetDetectedAndOverlookedEvents(parent);
            SubscribeOnParentRotatorEvents(parent);
        }
        void OnDisable()
        {
            UnsubscribeFromParentTargetDetectedAndOverlookedEvents();
            UnsubscribeFromParentRotatorEvents();
        }
        
        void SubscribeOnParentTargetDetectedAndOverlookedEvents(Transform parent)
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
        void SubscribeOnParentRotatorEvents(Transform parent)
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