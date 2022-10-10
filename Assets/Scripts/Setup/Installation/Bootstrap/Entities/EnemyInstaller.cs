using Roguelike.Core.Entities;
using Roguelike.Core.Gameplay.Events.Handling;
using Roguelike.Core.Gameplay.Events.Subscribing;
using Roguelike.Core.Gameplay.Interaction.Sensation;
using Roguelike.Core.Gameplay.Spawning;
using Roguelike.Core.Gameplay.Tracking;
using Roguelike.Core.Gameplay.Transformation.Moving;
using Roguelike.Core.Gameplay.Transformation.Rotation;
using Roguelike.Core.Information;
using Roguelike.Core.Setup.Placing;
using Roguelike.Gameplay.Interaction.Sensation;
using Roguelike.Gameplay.Tracking.Finishing;
using Roguelike.Setup.Loading;
using Roguelike.Setup.Placing;
using Roguelike.Utilities.Extensions.Extenject;
using UnityEngine;
using Zenject;

namespace Roguelike.Setup.Installation.Bootstrap.Entities
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
            BindStamina();
            BindEventHandler();
            BindTurnFinisher();
            BindInfo();
            BindTargetDetectionStatus();
            BindSalivator();
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
        void BindStamina()
        {
            Container
                .Bind<Stamina>()
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
        void BindSalivator()
        {
            Container
                .Bind<EnemySalivator>()
                .FromComponentOnParentContextGameObject()
                .AsTransient()
                .WhenInjectedInto<EnemyEventHandler>();
        }
        #endregion
    }
}