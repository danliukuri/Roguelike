using Roguelike.Core;
using Roguelike.Core.Placers;
using Roguelike.Utilities.Pools;
using UnityEngine;
using Zenject;

namespace Roguelike
{
    public class LevelLoader : IInitializable
    {
        #region Fields
        readonly Vector3 firstRoomPosition;
        readonly Transform environmentParent;
        int numberOfRooms;
        int numberOfKeys;
    
        int currentLevelNumber;
    
        readonly IDungeonPlacer dungeonPlacer;
        readonly PoolableObjectsReturner poolableObjectsReturner;
        readonly IResettable[] resettableComponents;
        #endregion
    
        #region Methods
        public LevelLoader(Vector3 firstRoomPosition, Transform environmentParent,
            int initialNumberOfRooms, int initialNumberOfKeys,
            IDungeonPlacer dungeonPlacer, PoolableObjectsReturner poolableObjectsReturner,
            IResettable[] resettableComponents)
        {
            this.firstRoomPosition = firstRoomPosition;
            this.environmentParent = environmentParent;
            this.numberOfRooms = initialNumberOfRooms;
            this.numberOfKeys = initialNumberOfKeys;
        
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
            numberOfRooms += currentLevelNumber;
            LoadLevel();
        }
        void LoadLevel()
        {
            dungeonPlacer.Place(firstRoomPosition, numberOfRooms, environmentParent, numberOfKeys);
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