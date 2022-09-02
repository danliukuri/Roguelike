using Roguelike.Core.Information;
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
        
        readonly LevelSettings levelSettings;
        readonly PoolableObjectsReturner poolableObjectsReturner;
        readonly Inventory playerInventory;
        #endregion
        
        #region Methods
        public SceneLoader(int levelSceneBuildIndex, int menusSceneBuildIndex,
            LevelSettings levelSettings, PoolableObjectsReturner poolableObjectsReturner, Inventory playerInventory)
        {
            this.levelSceneBuildIndex = levelSceneBuildIndex;
            this.menusSceneBuildIndex = menusSceneBuildIndex;
            
            this.levelSettings = levelSettings;
            this.poolableObjectsReturner = poolableObjectsReturner;
            this.playerInventory = playerInventory;
        }
        public void Initialize() => LoadMenusScene();
        
        public AsyncOperation LoadLevelScene(LoadSceneMode sceneMode = LoadSceneMode.Single) =>
            SceneManager.LoadSceneAsync(levelSceneBuildIndex, sceneMode);
        public AsyncOperation LoadMenusScene(LoadSceneMode sceneMode = LoadSceneMode.Single) =>
            SceneManager.LoadSceneAsync(menusSceneBuildIndex, sceneMode);
        public AsyncOperation UnloadLevelScene()
        {
            playerInventory.Reset();
            levelSettings.Reset();
            poolableObjectsReturner.ReturnAllToPool();
            return SceneManager.UnloadSceneAsync(levelSceneBuildIndex);
        }
        public AsyncOperation UnloadMenusScene() => SceneManager.UnloadSceneAsync(menusSceneBuildIndex);
        #endregion
    }
}