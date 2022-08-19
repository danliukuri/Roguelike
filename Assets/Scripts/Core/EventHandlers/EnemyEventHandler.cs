using System.Linq;
using Roguelike.Core.Information;
using Roguelike.Core.Movers;
using Roguelike.Core.Sensors;
using UnityEngine;

namespace Roguelike.Core.EventHandlers
{
    public class EnemyEventHandler
    {
        #region Fields
        readonly PathfindingEntityMover mover;
        readonly ISensor[] sensors;
        #endregion
        
        #region Methods
        public EnemyEventHandler(PathfindingEntityMover mover, ISensor[] sensors)
        {
            this.mover = mover;
            this.sensors = sensors;
        }
        
        public void OnPlayerActionCompleted(object sender, MovingEventArgs e)
        {
            Vector3 targetPosition = e.Destination;
            if (sensors.Any(sensor => sensor.IsInSensitivityRange(targetPosition)))
                mover.TryToMakeClosestMoveToTarget(targetPosition);
        }
        #endregion
    }
}