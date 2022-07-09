using Roguelike.Core.Factories;
using Roguelike.Utilities;
using Roguelike.Utilities.Extensions;
using Roguelike.Utilities.Pools;
using UnityEngine;

namespace Roguelike.Factories
{
    public class WallFactory : MonoBehaviour, IGameObjectFactory
    {
        #region Fields
        [SerializeField] ObjectsPool wallsPool;
        [SerializeField] WeightAndValue<Sprite>[] wallSprites;
        #endregion

        #region Methods
        public GameObject GetGameObject()
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