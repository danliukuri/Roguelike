using UnityEngine;

namespace Roguelike.Core.Services.Pathfinding
{
    public interface IPathfinder
    {
        Vector3 FindNextPosition(Vector3 startPosition, Vector3 targetPosition);
    }
}