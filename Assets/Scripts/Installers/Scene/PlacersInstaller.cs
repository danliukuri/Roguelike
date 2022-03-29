using Roguelike.Core.Placers;
using Roguelike.Placers;
using Zenject;

namespace Roguelike.Installers.Scene
{
    public class PlacersInstaller : MonoInstaller
    {
        #region Methods
        public override void InstallBindings()
        {
            BindRoomsPlacer();
        }

        void BindRoomsPlacer()
        {
            Container
                .Bind<IRoomsPlacer>()
                .To<RoomsPlacer>()
                .AsSingle();
        }
        #endregion
    }
}