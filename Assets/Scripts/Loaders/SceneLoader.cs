using Roguelike.Utilities.Pools;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Roguelike.Loaders
{
    public class SceneLoader : IInitializable
    {
        #region Fields
        readonly int levelSceneBuildIndex;
        readonly int menusSceneBuildIndex;
        
        readonly PoolableObjectsReturner poolableObjectsReturner;
        #endregion
        
        #region Methods
        public SceneLoader(int levelSceneBuildIndex, int menusSceneBuildIndex,
            PoolableObjectsReturner poolableObjectsReturner)
        {
            this.levelSceneBuildIndex = levelSceneBuildIndex;
            this.menusSceneBuildIndex = menusSceneBuildIndex;
            this.poolableObjectsReturner = poolableObjectsReturner;
        }
        public void Initialize() => LoadMenusScene();
        
        public AsyncOperation LoadLevelScene(LoadSceneMode sceneMode = LoadSceneMode.Single) =>
            SceneManager.LoadSceneAsync(levelSceneBuildIndex, sceneMode);
        public AsyncOperation LoadMenusScene(LoadSceneMode sceneMode = LoadSceneMode.Single) =>
            SceneManager.LoadSceneAsync(menusSceneBuildIndex, sceneMode);
        public AsyncOperation UnloadLevelScene()
        {
            poolableObjectsReturner.ReturnAllToPool();
            return SceneManager.UnloadSceneAsync(levelSceneBuildIndex);
        }
        public AsyncOperation UnloadMenusScene() => SceneManager.UnloadSceneAsync(menusSceneBuildIndex);
        #endregion
    }
}