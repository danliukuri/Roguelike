using Roguelike.Animators;
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
using UnityEngine;
using Zenject;

namespace Roguelike.Installers.Bootstrap
{
    public class EnemyInstaller : MonoInstaller
    {
        #region Fields
        [SerializeField] int numberOfActionsToUseStamina;
        [SerializeField] int numberOfActionsForRestoringStamina;
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
        }
        void BindMover()
        {
            Container
                .Bind<PathfindingEntityMover>()
                .FromComponentOn(GetParentContextGameObject)
                .AsTransient()
                .WhenInjectedInto<EnemyEventHandler>();
        }
        void BindRotator()
        {
            Container
                .Bind<SpriteRotator>()
                .FromComponentOn(GetParentContextGameObject)
                .AsTransient()
                .WhenInjectedInto<EnemyEventHandler>();
        }
        void BindSensors()
        {
            Container
                .Bind<ISensor>()
                .To(typeof(HearingSensor), typeof(VisionSensor))
                .FromComponentsOn(GetParentContextGameObject)
                .AsTransient()
                .WhenInjectedInto<EnemyEventHandler>();
        }
        void BindTargetMovingTracker()
        {
            Container
                .Bind<TargetMovingTracker>()
                .AsTransient()
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
            BindAnimatorParameterSetter();
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
            void BindAnimatorParameterSetter()
            {
                Container
                    .Bind<AnimatorParameter>()
                    .FromComponentOn(GetParentContextGameObject)
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
                    .FromComponentOn(GetParentContextGameObject)
                    .AsTransient()
                    .WhenInjectedInto<TargetDetectionStatusEventHandler>();
            }
        }
        
        GameObject GetParentContextGameObject(InjectContext context) => 
            ((Component)context.ParentContext.ObjectInstance).gameObject;
        #endregion
    }
}