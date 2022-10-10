using System;
using Roguelike.Core.Characteristics;
using UnityEngine;

namespace Roguelike.Core.Information
{
    [Serializable]
    public class LevelSettings : IResettable
    {
        #region Properties
        public int NumberOfRooms { get; set; }
        public int NumberOfKeys { get; set; }
        public int NumberOfEnemies { get; set; }
        public int LevelNumber
        {
            get => levelNumber;
            set
            {
                levelNumber = value;
                LevelNumberChanged?.Invoke(value);
            }
        }
        public RoomElementMarkersInfo[] RoomElementMarkersInfo { get; set; }
        #endregion
        
        #region Events
        public event Action<int> LevelNumberChanged;
        #endregion
        
        #region Fields
        [SerializeField] int numberOfRoomsAtFirstLevel;
        [SerializeField] int numberOfKeysAtFirstLevel;
        [SerializeField] int numberOfEnemiesAtFirstLevel;
        int levelNumber;
        #endregion
        
        #region Methods
        public LevelSettings Initialize()
        {
            NumberOfRooms = numberOfRoomsAtFirstLevel;
            NumberOfKeys = numberOfKeysAtFirstLevel;
            NumberOfEnemies = numberOfEnemiesAtFirstLevel;
            return this;
        }
        public void Reset()
        {
            Initialize();
            LevelNumber = default;
            RoomElementMarkersInfo = default;
        }
        #endregion
    }
}