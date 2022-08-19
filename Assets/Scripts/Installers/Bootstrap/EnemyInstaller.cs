using Roguelike.Core.EventHandlers;
using Roguelike.Core.EventSubscribers;
using Roguelike.Core.Movers;
using Roguelike.Core.Sensors;
using Roguelike.Sensors;
using Zenject;

namespace Roguelike.Installers.Bootstrap
{
    public class EnemyInstaller : MonoInstaller
    {
        #region Methods
        public override void InstallBindings()
        {
            BindMover();
            BindSensors();
            BindEventHandler();
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
        void BindEventHandler()
        {
            Container
                .Bind<EnemyEventHandler>()
                .AsTransient()
                .WhenInjectedInto<EnemyEventSubscriber>();
        }
        #endregion
    }
}