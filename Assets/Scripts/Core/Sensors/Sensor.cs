using UnityEngine;

namespace Roguelike.Core.Sensors
{
    public abstract class Sensor : MonoBehaviour, ISensor
    {
        #region Fields
        [SerializeField] protected int sensitivityRange;
        #endregion

        #region Methods
        public abstract bool IsInSensitivityRange(Vector3 targetPosition);
        #endregion
    }
}