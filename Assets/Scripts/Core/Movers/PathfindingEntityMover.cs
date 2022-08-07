using Roguelike.Core.Services.Pathfinding;
using Roguelike.Services.Pathfinding;
using UnityEngine;

namespace Roguelike.Core.Movers
{
    public class PathfindingEntityMover : EntityMover
    {
        #region Fields
        IPathfinder pathfinder;
        #endregion
        
        #region Methods
        void Awake() => pathfinder = new PathfindingAdapter(this);

        public void MakeClosestMoveTo(Vector3 targetPosition)
        {
            Vector3 startPosition = transform.position;
            Vector3 destination = pathfinder.FindNextPosition(startPosition, targetPosition);
            Vector3 translation = destination - startPosition;
            TryToMove(translation);
        }
        #endregion
    }
}