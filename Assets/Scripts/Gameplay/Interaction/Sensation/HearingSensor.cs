using System.Collections.Generic;
using Roguelike.Core.Gameplay.Interaction.Sensation;
using Roguelike.Core.Gameplay.Transformation.Moving;
using UnityEngine;

namespace Roguelike.Gameplay.Interaction.Sensation
{
    public class HearingSensor : Sensor
    {
        #region Fields
        PathfindingEntityMover pathfindingMover;
        #endregion
        
        #region Methods
        void Awake() => pathfindingMover = GetComponent<PathfindingEntityMover>();
        
        public override bool IsInSensitivityRange(Vector3 targetPosition)
        {
            bool isTargetHeard = false;
            if (CanHearTarget(targetPosition))
            {
                pathfindingMover.Pathfinder.ResetPath();
                List<Vector3> pathToTarget = pathfindingMover.Pathfinder.FindPath(transform.position, targetPosition);
                isTargetHeard = pathToTarget != default;
            }
            return isTargetHeard;
        }
        bool CanHearTarget(Vector3 targetPosition)
        {
            Vector3 startPosition = transform.position;
            float xDifference = Mathf.Abs(targetPosition.x - startPosition.x);
            float yDifference = Mathf.Abs(targetPosition.y - startPosition.y);
            return xDifference + yDifference <= sensitivityRange;
        }
        #endregion
    }
}