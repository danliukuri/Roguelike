using System.Collections.Generic;
using Roguelike.Core.Information;
using Roguelike.Utilities.Finding.Path;
using UnityEngine;

namespace Roguelike.Core.Gameplay.Transformation.Moving
{
    public class PathfindingEntityMover : EntityMover
    {
        #region Properties
        public IPathfinder Pathfinder { get; private set; }
        #endregion
        
        #region Fields
        [Header("Pathfinding abilities")]
        [SerializeField] bool canPavePathToWalls;
        [SerializeField] bool canPavePathToEnemies;
        [SerializeField] bool canPavePathToItems;
        [SerializeField] bool canPavePathToExits;
        [SerializeField] bool canPavePathToDoors;
        [SerializeField] int maxPathLength;
        MovingInfo[] generalMovingInfoForPathfinding;
        #endregion
        
        #region Methods
        void Awake() => Pathfinder = new PathfindingAdapter(this);
        public override void Reset()
        {
            base.Reset();
            generalMovingInfoForPathfinding = default;
            Pathfinder.ResetPath();
        }
        
        public bool TryToMakeClosestMoveToTarget(Vector3 targetPosition)
        {
            bool isMoved = false;
            Vector3 startPosition = transform.position;
            if (startPosition != targetPosition)
            {
                List<Vector3> pathToTarget = Pathfinder.FindPath(startPosition, targetPosition);
                Pathfinder.ResetPath();
                if (pathToTarget != default && pathToTarget.Count - 1 <= maxPathLength)
                {
                    int nextPositionIndex = pathToTarget.Count - 2;
                    Vector3 destination = pathToTarget[nextPositionIndex];
                    
                    Vector3 translation = destination - startPosition;
                    isMoved = TryToMove(translation);
                }
            }
            return isMoved;
        }
        public bool IsPathPossible(Vector3 destination) =>
            IsMovePossible(destination, GetGeneralMovingInfoForPathfinding());
        
        MovingInfo[] GetGeneralMovingInfoForPathfinding() => generalMovingInfoForPathfinding ??= new[]
        {
            new MovingInfo { ElementsByRoom = dungeonInfo.EnemiesByRoom,
                Args = new MovingEventArgs { IsMovePossible = canPavePathToEnemies } },
            new MovingInfo { ElementsByRoom = dungeonInfo.KeysByRoom,
                Args = new MovingEventArgs { IsMovePossible = canPavePathToItems } },
            new MovingInfo { ElementsByRoom = dungeonInfo.DoorsByRoom,
                Args = new MovingEventArgs { IsMovePossible = canPavePathToDoors } },
            new MovingInfo { ElementsByRoom = dungeonInfo.ExitsByRoom,
                Args = new MovingEventArgs { IsMovePossible = canPavePathToExits } },
            new MovingInfo { ElementsByRoom = dungeonInfo.WallsByRoom,
                Args = new MovingEventArgs { IsMovePossible = canPavePathToWalls } },
        };
        #endregion
    }
}