using Roguelike.Core.EventHandlers;
using Roguelike.Core.EventSubscribers;
using Roguelike.Core.Movers;
using Zenject;

namespace Roguelike.Installers.Bootstrap
{
    public class EnemyInstaller : MonoInstaller
    {
        #region Methods
        public override void InstallBindings()
        {
            BindMover();
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