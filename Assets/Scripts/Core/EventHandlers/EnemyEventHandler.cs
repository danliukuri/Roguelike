using System.Linq;
using Roguelike.Core.Information;
using Roguelike.Core.Movers;
using Roguelike.Core.Sensors;
using Roguelike.Finishers;

namespace Roguelike.Core.EventHandlers
{
    public class EnemyEventHandler
    {
        #region Fields
        readonly PathfindingEntityMover mover;
        readonly ISensor[] sensors;
        TurnFinisher turnFinisher;
        #endregion
        
        #region Methods
        public EnemyEventHandler(PathfindingEntityMover mover, ISensor[] sensors)
        {
            this.mover = mover;
            this.sensors = sensors;
        }
        public void SetTurnFinisher(TurnFinisher value) => turnFinisher = value;
        
        public void OnPlayerActionCompleted(object sender, MovingEventArgs e)
        {
            if (sensors.Any(sensor => sensor.IsInSensitivityRange(e.Destination)))
                turnFinisher.TryToFinish(e);
        }
        public void OnTurnFinished(object sender, MovingEventArgs e) =>
            mover.TryToMakeClosestMoveToTarget(e.Destination);
        #endregion
    }
}