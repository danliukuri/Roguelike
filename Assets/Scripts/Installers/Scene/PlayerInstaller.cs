using Roguelike.Core.EventSubscribers;
using Roguelike.Core.Information;
using Roguelike.Core.Movers;
using Roguelike.Core.Pickers;
using Roguelike.Pickers;
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
            BindKeyPicker();
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
        void BindKeyPicker()
        {
            Container
                .Bind<IPicker>()
                .To<KeyPicker>()
                .AsCached()
                .WhenInjectedInto<PlayerEventSubscriber>();
        }
        #endregion
    }
}