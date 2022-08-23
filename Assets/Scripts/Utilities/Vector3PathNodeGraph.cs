using System;
using System.Collections.Generic;
using System.Linq;
using AStarPathfinding;
using UnityEngine;

namespace Roguelike.Utilities
{
    public class Vector3PathNodeGraph : PathNodeGraph<Vector3>
    {
        #region Fields
        readonly Func<Vector3, bool> isPathPossible;
        readonly float distanceBetweenTwoConnectedNodes;
        #endregion
        
        #region Methods
        public Vector3PathNodeGraph(Func<Vector3, bool> isPathPossible, float distanceBetweenTwoConnectedNodes) : 
            base((first, second) => first == second)
        {
            this.isPathPossible = isPathPossible;
            this.distanceBetweenTwoConnectedNodes = distanceBetweenTwoConnectedNodes;
        }
        
        public override float CostToTarget(Vector3 node, Vector3 target) => (target - node).magnitude;
        public override float CostToConnectedNode(Vector3 node, Vector3 connectedNode)
        {
            int fromIndex = (int)(node.x + node.y);
            bool isMoveEven = fromIndex % 2 == 0;
            bool isMoveOdd = !isMoveEven;
            
            bool isItYMovement = !Mathf.Approximately(node.x, connectedNode.x);
            bool isItXMovement = !Mathf.Approximately(node.y, connectedNode.y);
                
            bool isNeededToMakeMovementSlightlyMoreExpensive = 
                (isMoveEven && isItYMovement) || (isMoveOdd && isItXMovement);
            
            float cost = distanceBetweenTwoConnectedNodes;
            if (isNeededToMakeMovementSlightlyMoreExpensive)
            {
                const float movementPenalty = 0.001f;
                cost += movementPenalty;
            }
            return cost;
        }
        public override IEnumerable<Vector3> GetConnectedNodes(Vector3 node)
        {
            Vector3[] neighborPositions =
            {
                new Vector3(node.x, node.y + distanceBetweenTwoConnectedNodes),
                new Vector3(node.x, node.y - distanceBetweenTwoConnectedNodes),
                new Vector3(node.x + distanceBetweenTwoConnectedNodes, node.y),
                new Vector3(node.x - distanceBetweenTwoConnectedNodes, node.y)
            };
            return neighborPositions.Where(neighborPosition => isPathPossible(neighborPosition));
        }
        #endregion
    }
}