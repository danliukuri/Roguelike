using Roguelike.Core.Factories;
using Roguelike.Movers;
using Roguelike.Utilities;
using Roguelike.Utilities.Extensions;
using UnityEngine;
using Zenject;

namespace Roguelike.Factories
{
    public class PlayerFactory: MonoBehaviour, IPlayerFactory
    {
        #region Fields
        [SerializeField] ObjectsPool playersPool;
        [SerializeField] WeightAndValue<Sprite>[] playersSprites;
        #endregion

        #region Methods
        public GameObject GetPlayer()
        {
            GameObject playerGameObject = playersPool.GetFreeObject();

            SpriteRenderer playerSpriteRenderer = playerGameObject.GetComponent<SpriteRenderer>();
            playerSpriteRenderer.sprite = playersSprites.RandomBasedOnWeights().Value;

            return playerGameObject;
        }
        #endregion
    }
}