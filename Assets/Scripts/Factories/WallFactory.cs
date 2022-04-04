using Roguelike.Core.Factories;
using Roguelike.Utilities;
using Roguelike.Utilities.Extensions;
using UnityEngine;

namespace Roguelike.Factories
{
    public class WallFactory : MonoBehaviour, IWallFactory
    {
        #region Fields
        [SerializeField] ObjectsPool wallsPool;
        [SerializeField] WeightAndValue<Sprite>[] wallSprites;
        #endregion

        #region Methods
        public GameObject GetWall()
        {
            GameObject wallGameObject = wallsPool.GetFreeObject();
            
            SpriteRenderer wallSpriteRenderer = wallGameObject.GetComponent<SpriteRenderer>();
            wallSpriteRenderer.sprite = wallSprites.RandomBasedOnWeights().Value;

            Transform wallTransform = wallGameObject.transform;
            wallTransform.RotateRandomNumberOfTimesByRightAngle(wallTransform.forward);

            return wallGameObject;
        }
        #endregion
    }
}