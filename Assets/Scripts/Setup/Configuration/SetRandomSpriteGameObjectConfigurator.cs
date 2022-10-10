using Roguelike.Core.Setup.Configuration;
using Roguelike.Utilities.Extensions.Generic;
using Roguelike.Utilities.Generic;
using UnityEngine;

namespace Roguelike.Setup.Configuration
{
    public class SetRandomSpriteGameObjectConfigurator : MonoBehaviour,
        IConfigurator<GameObject>, IConfigurator<SpriteRenderer>
    {
        #region Fields
        [SerializeField] WeightAndValue<Sprite>[] spritesToPickFrom;
        #endregion
        
        #region Methods
        public GameObject Configure(GameObject objectToConfigure)
        {
            SetRandomSprite(objectToConfigure);
            return objectToConfigure;
        }
        public SpriteRenderer Configure(SpriteRenderer rendererToConfigure)
        {
            SetRandomSprite(rendererToConfigure);
            return rendererToConfigure;
        }
        
        public Sprite SetRandomSprite(GameObject objectToSetRandomSprite) =>
            SetRandomSprite(objectToSetRandomSprite.GetComponent<SpriteRenderer>());
        public Sprite SetRandomSprite(SpriteRenderer objectSpriteRenderer) =>
            objectSpriteRenderer.sprite = spritesToPickFrom.RandomBasedOnWeights().Value;
        #endregion
    }
}