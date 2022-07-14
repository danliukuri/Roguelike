using Roguelike.Core.EventSubscribers;
using Roguelike.Core.Information;
using Roguelike.Core.Movers;
using Roguelike.Core.Pickers;
using Zenject;

namespace Roguelike.Installers.Scene
{
    public class PlayerInstaller : MonoInstaller
    {
        #region Methods
        public override void InstallBindings()
        {
            BindMover();
            BindInventory();
            BindItemsPicker();
        }
        void BindMover()
        {
            Container
                .Bind<EntityMover>()
                .FromComponentSibling()
                .AsCached();
        }
        void BindInventory()
        {
            Container
                .Bind<Inventory>()
                .AsCached()
                .When(context => context.ParentContext.ObjectType == typeof(PlayerEventSubscriber));
        }
        void BindItemsPicker()
        {
            Container
                .Bind<ItemsPicker>()
                .AsCached()
                .WhenInjectedInto<PlayerEventSubscriber>();
        }
        #endregion
    }
}