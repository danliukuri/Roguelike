using Roguelike.Core.Information;
using UnityEngine;
using Zenject;

namespace Roguelike.Installers.Bootstrap
{
    public class LevelSettingsInstaller : MonoInstaller
    {
        #region Fields
        [SerializeField] LevelSettings levelSettings;
        #endregion
        
        #region Methods
        public override void InstallBindings()
        {
            BindInstance();
            BindUpdater();
        }
        void BindInstance() => Container.BindInstance(levelSettings);
        void BindUpdater()
        {
            Container
                .Bind<LevelSettingsUpdater>()
                .FromNew()
                .AsSingle();
        }
        #endregion
    }
}