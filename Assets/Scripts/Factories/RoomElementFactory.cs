using Roguelike.Core.Factories;
using Roguelike.Utilities;
using Roguelike.Utilities.Extensions;
using Roguelike.Utilities.Pools;
using UnityEngine;

namespace Roguelike.Factories
{
    public class RoomElementFactory : MonoBehaviour, IGameObjectFactory
    {
        #region Fields
        [SerializeField] ObjectsPool roomElementsPool;
        [SerializeField] WeightAndValue<Sprite>[] roomElementSprites;
        #endregion

        #region Methods
        public virtual GameObject GetGameObject()
        {
            GameObject roomElementGameObject = roomElementsPool.GetFreeObject();

            SpriteRenderer roomElementsSpriteRenderer = roomElementGameObject.GetComponent<SpriteRenderer>();
            roomElementsSpriteRenderer.sprite = roomElementSprites.RandomBasedOnWeights().Value;

            return roomElementGameObject;
        }
        #endregion
    }
}