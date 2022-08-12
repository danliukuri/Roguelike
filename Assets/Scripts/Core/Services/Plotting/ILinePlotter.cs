using UnityEngine;

namespace Roguelike.Core.Services.Plotting
{
    public interface ILinePlotter
    {
        bool CanLineBePlotted(Vector3 startPosition, Vector3 targetPosition);
    }
}