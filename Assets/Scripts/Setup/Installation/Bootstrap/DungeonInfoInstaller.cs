using Roguelike.Core.Information;
using Zenject;

namespace Roguelike.Setup.Installation.Bootstrap
{
    public class DungeonInfoInstaller : MonoInstaller
    {
        #region Methods
        public override void InstallBindings()
        {
            BindDungeonInfo();
        }
        void BindDungeonInfo()
        {
            Container
                .Bind<DungeonInfo>()
                .AsSingle();
        }
        #endregion
    }
}