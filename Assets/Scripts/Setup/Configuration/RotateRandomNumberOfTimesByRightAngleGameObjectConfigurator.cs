using Roguelike.Core.Setup.Configuration;
using Roguelike.Utilities.Extensions.Unity;
using UnityEngine;

namespace Roguelike.Setup.Configuration
{
    public class RotateRandomNumberOfTimesByRightAngleGameObjectConfigurator : MonoBehaviour,
        IConfigurator<GameObject>, IConfigurator<Transform>
    {
        #region Methods
        public GameObject Configure(GameObject objectToConfigure) =>
            RotateRandomNumberOfTimesByRightAngle(objectToConfigure);
        public Transform Configure(Transform transformToConfigure) =>
            RotateRandomNumberOfTimesByRightAngle(transformToConfigure);
        
        public GameObject RotateRandomNumberOfTimesByRightAngle(GameObject objectToRotate)
        {
            RotateRandomNumberOfTimesByRightAngle(objectToRotate.transform);
            return objectToRotate;
        }
        public Transform RotateRandomNumberOfTimesByRightAngle(Transform objectTransform) =>
            objectTransform.RotateRandomNumberOfTimesByRightAngle(objectTransform.forward);
        #endregion
    }
}