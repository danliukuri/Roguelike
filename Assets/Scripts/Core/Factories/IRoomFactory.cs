using UnityEngine;

namespace Roguelike.Core.Factories
{
    public interface IRoomFactory
    {
        #region Methods
        GameObject GetRoom();
        #endregion
    }
}