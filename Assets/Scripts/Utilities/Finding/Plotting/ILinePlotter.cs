using UnityEngine;

namespace Roguelike.Utilities.Finding.Plotting
{
    public interface ILinePlotter
    {
        bool CanLineBePlotted(Vector3 startPosition, Vector3 targetPosition);
    }
}