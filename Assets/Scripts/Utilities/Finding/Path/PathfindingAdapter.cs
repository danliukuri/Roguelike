using System.Collections.Generic;
using AStarPathfinding;
using Roguelike.Core.Gameplay.Transformation.Moving;
using UnityEngine;

namespace Roguelike.Utilities.Finding.Path
{
    public class PathfindingAdapter : IPathfinder
    {
        #region Fields
        readonly AStar<Vector3> aStar;
        List<Vector3> foundPath;
        #endregion
        
        #region Methods
        public PathfindingAdapter(PathfindingEntityMover mover) => 
            aStar = new AStar<Vector3>(new Vector3PathNodeGraph(mover.IsPathPossible, mover.MovementStep));
        
        public List<Vector3> FindPath(Vector3 startPosition, Vector3 targetPosition) =>
            foundPath ??= aStar.ShortestReversedPathOrDefault(startPosition, targetPosition);
        public void ResetPath() => foundPath = default;
        #endregion
    }
}