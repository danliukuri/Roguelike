using Roguelike.Utilities;
using Roguelike.Utilities.Extensions;
using UnityEngine;

namespace Roguelike.Factories
{
    public class RoomElementFactory : GameObjectFactory
    {
        #region Fields
        [SerializeField] WeightAndValue<Sprite>[] roomElementSprites;
        #endregion
        
        #region Methods
        public override GameObject GetGameObject()
        {
            GameObject roomElementGameObject = base.GetGameObject();
            
            SpriteRenderer roomElementsSpriteRenderer = roomElementGameObject.GetComponent<SpriteRenderer>();
            roomElementsSpriteRenderer.sprite = roomElementSprites.RandomBasedOnWeights().Value;
            
            return roomElementGameObject;
        }
        #endregion
    }
}