using Roguelike.Core;
using Roguelike.Core.Information;
using Roguelike.Core.Placers;
using Roguelike.Utilities.Pools;
using Zenject;

namespace Roguelike.Loaders
{
    public class LevelLoader : IInitializable
    {
        #region Fields
        readonly LevelSettings levelSettings;
        readonly LevelSettingsUpdater levelSettingsUpdater;
        
        readonly IDungeonPlacer dungeonPlacer;
        readonly PoolableObjectsReturner poolableObjectsReturner;
        readonly IResettable[] resettableComponents;
        
        int currentLevelNumber;
        #endregion
        
        #region Methods
        public LevelLoader(LevelSettings levelSettings, LevelSettingsUpdater levelSettingsUpdater,
            IDungeonPlacer dungeonPlacer,
            PoolableObjectsReturner poolableObjectsReturner, IResettable[] resettableComponents)
        {
            this.levelSettings = levelSettings;
            this.levelSettingsUpdater = levelSettingsUpdater;
            this.dungeonPlacer = dungeonPlacer;
            this.poolableObjectsReturner = poolableObjectsReturner;
            this.resettableComponents = resettableComponents;
        }
        public void Initialize() => LoadLevel();
        
        public void LoadNextLevel()
        {
            UnLoadCurrentLevel();
            LoadLevel();
        }
        void LoadLevel()
        {
            levelSettingsUpdater.Update();
            dungeonPlacer.Place(levelSettings);
        }
        void UnLoadCurrentLevel()
        {
            poolableObjectsReturner.ReturnAllToPool();
            for (int i = 0; i < resettableComponents.Length; i++)
                resettableComponents[i].Reset();
        }
        #endregion
    }
}