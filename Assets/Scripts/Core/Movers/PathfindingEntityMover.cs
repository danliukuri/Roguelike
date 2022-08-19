using System.Collections.Generic;
using Roguelike.Core.Services.Pathfinding;
using Roguelike.Services.Pathfinding;
using UnityEngine;

namespace Roguelike.Core.Movers
{
    [RequireComponent(typeof(EntityMover))]
    public class PathfindingEntityMover : MonoBehaviour
    {
        #region Properties
        public IPathfinder Pathfinder { get; private set; }
        #endregion
        
        #region Fields
        EntityMover mover;
        #endregion
        
        #region Methods
        void Awake()
        {
            mover = GetComponent<EntityMover>();
            Pathfinder = new PathfindingAdapter(mover);
        }
        
        public bool TryToMakeClosestMoveToTarget(Vector3 targetPosition)
        {
            bool isMoved = false;
            Vector3 startPosition = transform.position;
            if (startPosition != targetPosition)
            {
                List<Vector3> pathToTarget = Pathfinder.FindPath(startPosition, targetPosition);
                Pathfinder.ResetPath();
                
                int nextPositionIndex = pathToTarget.Count - 2;
                Vector3 destination = pathToTarget[nextPositionIndex];

                Vector3 translation = destination - startPosition;
                isMoved = mover.TryToMove(translation);
            }
            return isMoved;
        }
        #endregion
    }
}