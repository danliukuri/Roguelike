using System.Linq;
using Roguelike.Core.Information;
using Roguelike.Core.Movers;
using Roguelike.Core.Rotators;
using Roguelike.Core.Sensors;
using Roguelike.Core.Services.Managers;
using Roguelike.Core.Services.Trackers;
using Roguelike.Finishers;

namespace Roguelike.Core.EventHandlers
{
    public class EnemyEventHandler : IResettable
    {
        #region Fields
        readonly PathfindingEntityMover mover;
        readonly SpriteRotator rotator;
        readonly ISensor[] sensors;
        readonly TargetMovingTracker targetMovingTracker;
        readonly StaminaManager staminaManager;
        TurnFinisher turnFinisher;
        #endregion
        
        #region Methods
        public EnemyEventHandler(PathfindingEntityMover mover, SpriteRotator rotator, ISensor[] sensors,
            TargetMovingTracker targetMovingTracker, StaminaManager staminaManager)
        {
            this.mover = mover;
            this.rotator = rotator;
            this.sensors = sensors;
            this.targetMovingTracker = targetMovingTracker;
            this.staminaManager = staminaManager;
        }
        public void Reset() => targetMovingTracker.Reset();
        public void SetTurnFinisher(TurnFinisher value) => turnFinisher = staminaManager.turnFinisher = value;
        
        public void OnMoving(object sender, MovingEventArgs e)
        {
            if (!rotator.TryToRotateRightOrLeft(e.Destination))
                rotator.TryToRotateRightOrLeft(targetMovingTracker.TargetMovingEventArgs.Destination);
        }
        public void OnPlayerActionCompleted(object sender, MovingEventArgs e)
        {
            if (sensors.Any(sensor => sensor.IsInSensitivityRange(e.Destination)))
                targetMovingTracker.TargetMovingEventArgs = e;
            
            if (!targetMovingTracker.IsTargetOnPosition(mover.transform.position))
                if (turnFinisher.TryToFinish(targetMovingTracker.TargetMovingEventArgs))
                    if (!staminaManager.TryToUse())
                        staminaManager.TryToRestore();
        }
        public void OnTurnFinished(object sender, MovingEventArgs e) =>
            mover.TryToMakeClosestMoveToTarget(e.Destination);
        #endregion
    }
}