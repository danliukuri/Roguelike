using Roguelike.Core.Factories;
using Roguelike.Utilities;
using Roguelike.Utilities.Extensions;
using Roguelike.Utilities.Pools;
using UnityEngine;

namespace Roguelike.Factories
{
    public class PlayerFactory: MonoBehaviour, IGameObjectFactory
    {
        #region Fields
        [SerializeField] ObjectsPool playersPool;
        [SerializeField] WeightAndValue<Sprite>[] playersSprites;
        #endregion

        #region Methods
        public GameObject GetGameObject()
        {
            GameObject playerGameObject = playersPool.GetFreeObject();

            SpriteRenderer playerSpriteRenderer = playerGameObject.GetComponent<SpriteRenderer>();
            playerSpriteRenderer.sprite = playersSprites.RandomBasedOnWeights().Value;

            return playerGameObject;
        }
        #endregion
    }
}