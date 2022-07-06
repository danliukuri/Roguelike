using Roguelike.Core.Entities;
using Zenject;

namespace Roguelike.Installers.Scene
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