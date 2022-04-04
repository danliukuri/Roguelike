using UnityEngine;

namespace Roguelike.Core.Factories
{
    public interface IWallFactory
    {
        #region Methods
        GameObject GetWall();
        #endregion
    }
}