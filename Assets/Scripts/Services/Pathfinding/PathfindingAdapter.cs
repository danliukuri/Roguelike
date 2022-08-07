using System.Collections.Generic;
using AStarPathfinding;
using Roguelike.Core.Movers;
using Roguelike.Core.Services.Pathfinding;
using Roguelike.Utilities;
using UnityEngine;

namespace Roguelike.Services.Pathfinding
{
    public class PathfindingAdapter : IPathfinder
    {
        #region Fields
        readonly AStar<Vector3> aStar;
        #endregion
        
        #region Methods
        public PathfindingAdapter(EntityMover mover) => aStar = new AStar<Vector3>(new Vector3PathNodeGraph(mover));
        
        public Vector3 FindNextPosition(Vector3 startPosition, Vector3 targetPosition)
        {
            if (startPosition == targetPosition)
                return targetPosition;
            
            List<Vector3> path = aStar.ShortestReversedPathOrDefault(startPosition, targetPosition);
            int nextPositionIndex = path.Count - 2;
            
            return path[nextPositionIndex];
        }
        #endregion
    }
}