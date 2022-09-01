using Roguelike.Core.EventHandlers;
using Roguelike.Core.EventSubscribers;
using Roguelike.Core.Information;
using Roguelike.Core.Movers;
using Roguelike.Core.Sensors;
using Roguelike.Core.Services.Managers;
using Roguelike.Core.Services.Trackers;
using Roguelike.Finishers;
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
            BindSensors();
            BindTargetMovingTracker();
            BindStaminaManager();
            BindEventHandler();
            BindTurnFinisher();
            BindInfo();
        }
        void BindMover()
        {
            Container
                .Bind<PathfindingEntityMover>()
                .FromComponentOn(context => ((EnemyEventSubscriber)context.ParentContext.ObjectInstance).gameObject)
                .AsTransient()
                .WhenInjectedInto<EnemyEventHandler>();
        }
        void BindSensors()
        {
            Container
                .Bind<ISensor>()
                .To(typeof(HearingSensor), typeof(VisionSensor))
                .FromComponentsOn(context => ((EnemyEventSubscriber)context.ParentContext.ObjectInstance).gameObject)
                .AsTransient()
                .WhenInjectedInto<EnemyEventHandler>();
        }
        void BindTargetMovingTracker()
        {
            Container
                .Bind<TargetMovingTracker>()
                .AsTransient()
                .WhenInjectedInto<EnemyEventHandler>();
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
        #endregion
    }
}