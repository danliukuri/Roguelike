using Roguelike.Core;
using Roguelike.Core.EventHandlers;
using Roguelike.Core.EventSubscribers;
using Roguelike.Core.Information;
using Roguelike.Core.Movers;
using Roguelike.Core.Placers;
using Roguelike.Core.Rotators;
using Roguelike.Core.Sensors;
using Roguelike.Core.Services.Managers;
using Roguelike.Core.Services.Trackers;
using Roguelike.Finishers;
using Roguelike.Loaders;
using Roguelike.Placers;
using Roguelike.Sensors;
using Roguelike.Utilities.Extensions.Extenject;
using UnityEngine;
using Zenject;

namespace Roguelike.Installers.Bootstrap
{
    public class EnemyInstaller : MonoInstaller
    {
        #region Fields
        [SerializeField] int numberOfActionsToUseStamina;
        [SerializeField] int numberOfActionsForRestoringStamina;
        [SerializeField] int requiredNumberOfTriesToOverlookTarget;
        #endregion
        
        #region Methods
        public override void InstallBindings()
        {
            BindMover();
            BindRotator();
            BindSensors();
            BindTargetMovingTracker();
            BindStaminaManager();
            BindEventHandler();
            BindTurnFinisher();
            BindInfo();
            BindTargetDetectionStatus();
            BindMouth();
        }
        void BindMover()
        {
            Container
                .Bind<PathfindingEntityMover>()
                .FromComponentOnParentContextGameObject()
                .AsTransient()
                .WhenInjectedInto<EnemyEventHandler>();
        }
        void BindRotator()
        {
            Container
                .Bind<SpriteRotator>()
                .FromComponentOnParentContextGameObject()
                .AsTransient()
                .WhenInjectedInto<EnemyEventHandler>();
        }
        void BindSensors()
        {
            Container
                .Bind<ISensor>()
                .To(typeof(HearingSensor), typeof(VisionSensor))
                .FromComponentOnParentContextGameObject()
                .AsTransient()
                .WhenInjectedInto<EnemyEventHandler>();
        }
        void BindTargetMovingTracker()
        {
            Container
                .Bind<TargetMovingTracker>()
                .AsTransient()
                .WithArguments(requiredNumberOfTriesToOverlookTarget)
                .WhenInjectedInto<EnemyEventSubscriber>();
        }
        void BindStaminaManager()
        {
            Container
                .Bind<StaminaManager>()
                .AsTransient()
                .WithArguments(numberOfActionsToUseStamina, numberOfActionsForRestoringStamina)
                .WhenInjectedInto<EnemyEventHandler>();
        }
        void BindTurnFinisher()
        {
            Container
                .Bind<TurnFinisher>()
                .AsTransient()
                .WhenInjectedInto<EnemyEventSubscriber>();
        }
        void BindEventHandler()
        {
            Container
                .Bind<EnemyEventHandler>()
                .AsTransient()
                .WhenInjectedInto<EnemyEventSubscriber>();
        }
        void BindInfo()
        {
            Container
                .Bind<EnemiesInfo>()
                .AsSingle();
        }
        void BindTargetDetectionStatus()
        {
            BindEventHandler();
            BindAnimator();
            BindPlacer();
            BindSpriteRotator();
            
            void BindEventHandler()
            {
                Container
                    .Bind<TargetDetectionStatusEventHandler>()
                    .FromNew()
                    .AsTransient()
                    .WhenInjectedInto<TargetDetectionStatusEventSubscriber>();
            }
            void BindAnimator()
            {
                Container
                    .Bind<Animator>()
                    .FromComponentOnParentContextGameObject()
                    .AsTransient()
                    .WhenInjectedInto<TargetDetectionStatusEventHandler>();
            }
            void BindPlacer()
            {
                Container
                    .Bind<IGameObjectPlacer>()
                    .To<ChildPlacer>()
                    .FromComponentSibling()
                    .AsTransient()
                    .WhenInjectedInto<GameObjectLoader>();
            }
            void BindSpriteRotator()
            {
                Container
                    .Bind<SpriteRotator>()
                    .FromComponentOnParentContextGameObject()
                    .AsTransient()
                    .WhenInjectedInto<TargetDetectionStatusEventHandler>();
            }
        }
        void BindMouth()
        {
            Container
                .Bind<EnemyMouth>()
                .FromComponentOnParentContextGameObject()
                .AsTransient()
                .WhenInjectedInto<EnemyEventHandler>();
        }
        #endregion
    }
}