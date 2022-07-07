using Roguelike.Core.Factories;
using Roguelike.Utilities;
using Roguelike.Utilities.Extensions;
using Roguelike.Utilities.Pools;
using UnityEngine;

namespace Roguelike.Factories
{
    public class ExitFactory : MonoBehaviour, IExitFactory
    {
        #region Fields
        [SerializeField] ObjectsPool exitsPool;
        [SerializeField] WeightAndValue<Sprite>[] exitSprites;
        #endregion

        #region Methods
        public GameObject GetExit()
        {
            GameObject exitGameObject = exitsPool.GetFreeObject();

            SpriteRenderer exitSpriteRenderer = exitGameObject.GetComponent<SpriteRenderer>();
            exitSpriteRenderer.sprite = exitSprites.RandomBasedOnWeights().Value;

            return exitGameObject;
        }
        #endregion
    }
}