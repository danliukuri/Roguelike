using UnityEngine;

namespace Roguelike.Core.Sensors
{
    public interface ISensor
    {
        bool IsInSensitivityRange(Vector3 targetPosition);
    }
}