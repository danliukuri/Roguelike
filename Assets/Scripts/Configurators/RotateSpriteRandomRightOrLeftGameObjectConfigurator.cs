using Roguelike.Core.Configurators;
using Roguelike.Core.Rotators;
using Roguelike.Utilities.Extensions;
using UnityEngine;

namespace Roguelike.Configurators
{
    public class RotateSpriteRandomRightOrLeftGameObjectConfigurator : MonoBehaviour,
        IConfigurator<GameObject>, IConfigurator<SpriteRotator>
    {
        #region Methods
        public GameObject Configure(GameObject objectToConfigure) => RotateSpriteRandomRightOrLeft(objectToConfigure);
        public SpriteRotator Configure(SpriteRotator rotatorToConfigure) =>
            RotateeSpriteRandomRightOrLeft(rotatorToConfigure);
        
        public GameObject RotateSpriteRandomRightOrLeft(GameObject objectToRotate)
        {
            RotateeSpriteRandomRightOrLeft(objectToRotate.GetComponent<SpriteRotator>());
            return objectToRotate;
        }
        public SpriteRotator RotateeSpriteRandomRightOrLeft(SpriteRotator objectSpriteRotator) =>
            objectSpriteRotator.RandomRotateRightOrLeft();
        #endregion
    }
}