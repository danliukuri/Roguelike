using UnityEngine;

namespace Roguelike.Core.Gameplay.Interaction.Sensation
{
    public interface ISensor
    {
        bool IsInSensitivityRange(Vector3 targetPosition);
    }
}