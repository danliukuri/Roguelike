using Roguelike.Utilities.Extensions;
using UnityEngine;

namespace Roguelike.Factories
{
    public class WallFactory : RoomElementFactory
    {
        #region Methods
        public override GameObject GetGameObject()
        {
            GameObject wallGameObject = base.GetGameObject();
            
            Transform wallTransform = wallGameObject.transform;
            wallTransform.RotateRandomNumberOfTimesByRightAngle(wallTransform.forward);
            
            return wallGameObject;
        }
        #endregion
    }
}