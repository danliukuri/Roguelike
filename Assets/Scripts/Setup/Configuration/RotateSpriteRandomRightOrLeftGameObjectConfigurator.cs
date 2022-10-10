using Roguelike.Core.Gameplay.Transformation.Rotation;
using Roguelike.Core.Setup.Configuration;
using Roguelike.Utilities.Extensions.Generic;
using UnityEngine;

namespace Roguelike.Setup.Configuration
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