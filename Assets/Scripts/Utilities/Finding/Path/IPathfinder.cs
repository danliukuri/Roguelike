using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Utilities.Finding.Path
{
    public interface IPathfinder
    {
        List<Vector3> FindPath(Vector3 startPosition, Vector3 targetPosition);
        void ResetPath();
    }
}