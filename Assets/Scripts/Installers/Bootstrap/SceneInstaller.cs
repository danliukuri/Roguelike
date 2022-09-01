using Roguelike.Loaders;
using UnityEngine;
using Zenject;

namespace Roguelike.Installers.Bootstrap
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