using System;
using UnityEngine;

namespace Roguelike.Core.Information
{
    [Serializable]
    public class LevelSettings
    {
        #region Properties
        [field: SerializeField] public int NumberOfRooms { get; set; }
        [field: SerializeField] public int NumberOfKeys { get; set; }
        [field: SerializeField] public int NumberOfEnemies { get; set; }
        #endregion
    }
}