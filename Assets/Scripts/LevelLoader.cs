using Roguelike.Core;
using Roguelike.Core.Information;
using Roguelike.Core.Placers;
using Roguelike.Utilities.Pools;
using Zenject;

namespace Roguelike
{
    public class LevelLoader : IInitializable
    {
        #region Fields
        readonly LevelSettings levelSettings;
        
        int currentLevelNumber;
        
        readonly IDungeonPlacer dungeonPlacer;
        readonly PoolableObjectsReturner poolableObjectsReturner;
        readonly IResettable[] resettableComponents;
        #endregion
        
        #region Methods
        public LevelLoader(LevelSettings levelSettings, IDungeonPlacer dungeonPlacer,
            PoolableObjectsReturner poolableObjectsReturner, IResettable[] resettableComponents)
        {
            this.levelSettings = levelSettings;
            this.dungeonPlacer = dungeonPlacer;
            this.poolableObjectsReturner = poolableObjectsReturner;
            this.resettableComponents = resettableComponents;
        }
        public void Initialize()
        {
            LoadLevel();
        }
        
        public void LoadNextLevel()
        {
            UnLoadCurrentLevel();
            levelSettings.NumberOfRooms += currentLevelNumber;
            LoadLevel();
        }
        void LoadLevel()
        {
            dungeonPlacer.Place(levelSettings);
            currentLevelNumber++;
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