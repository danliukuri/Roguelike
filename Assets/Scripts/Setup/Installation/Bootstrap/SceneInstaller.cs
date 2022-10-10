using Roguelike.Setup.Loading;
using UnityEngine;
using Zenject;

namespace Roguelike.Setup.Installation.Bootstrap
{
    public class SceneInstaller : MonoInstaller
    {
        #region Fields
        [SerializeField] int levelSceneBuildIndex, menusSceneBuildIndex;
        #endregion
        
        #region Methods
        public override void InstallBindings()
        {
            BindSceneLoader();
        }
        void BindSceneLoader()
        {
            Container
                .BindInterfacesAndSelfTo<SceneLoader>()
                .FromNew()
                .AsSingle()
                .WithArguments(levelSceneBuildIndex, menusSceneBuildIndex)
                .NonLazy();
        }
        #endregion
    }
}