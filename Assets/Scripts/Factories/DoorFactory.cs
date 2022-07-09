using Roguelike.Core.Factories;
using Roguelike.Utilities;
using Roguelike.Utilities.Extensions;
using Roguelike.Utilities.Pools;
using UnityEngine;

namespace Roguelike.Factories
{
    public class DoorFactory : MonoBehaviour, IGameObjectFactory
    {
        #region Fields
        [SerializeField] ObjectsPool doorsPool;
        [SerializeField] WeightAndValue<Sprite>[] doorSprites;
        #endregion

        #region Methods
        public GameObject GetGameObject()
        {
            GameObject doorGameObject = doorsPool.GetFreeObject();

            SpriteRenderer doorSpriteRenderer = doorGameObject.GetComponent<SpriteRenderer>();
            doorSpriteRenderer.sprite = doorSprites.RandomBasedOnWeights().Value;

            return doorGameObject;
        }
        #endregion
    }
}