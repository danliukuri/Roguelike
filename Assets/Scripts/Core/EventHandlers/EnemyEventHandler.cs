using System.Linq;
using Roguelike.Core.Information;
using Roguelike.Core.Movers;
using Roguelike.Core.Sensors;
using Roguelike.Core.Services.Trackers;
using Roguelike.Finishers;

namespace Roguelike.Core.EventHandlers
{
    public class EnemyEventHandler : IResettable
    {
        #region Fields
        readonly PathfindingEntityMover mover;
        readonly ISensor[] sensors;
        readonly TargetMovingTracker targetMovingTracker;
        TurnFinisher turnFinisher;
        #endregion
        
        #region Methods
        public EnemyEventHandler(PathfindingEntityMover mover, ISensor[] sensors,
            TargetMovingTracker targetMovingTracker)
        {
            this.mover = mover;
            this.sensors = sensors;
            this.targetMovingTracker = targetMovingTracker;
        }
        public void Reset() => targetMovingTracker.Reset();
        public void SetTurnFinisher(TurnFinisher value) => turnFinisher = value;

        public void OnPlayerActionCompleted(object sender, MovingEventArgs e)
        {
            if (sensors.Any(sensor => sensor.IsInSensitivityRange(e.Destination)))
                targetMovingTracker.TargetMovingEventArgs = e;
            
            if (!targetMovingTracker.IsTargetOnPosition(mover.transform.position))
                turnFinisher.TryToFinish(targetMovingTracker.TargetMovingEventArgs);
        }
        public void OnTurnFinished(object sender, MovingEventArgs e) =>
            mover.TryToMakeClosestMoveToTarget(e.Destination);
        #endregion
    }
}