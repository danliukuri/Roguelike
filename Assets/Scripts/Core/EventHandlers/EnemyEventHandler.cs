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
        readonly StaminaManager staminaManager;
        readonly EnemyMouth enemyMouth;
        TargetMovingTracker targetMovingTracker;
        TurnFinisher turnFinisher;
        #endregion
        
        #region Methods
        public EnemyEventHandler(PathfindingEntityMover mover, SpriteRotator rotator, ISensor[] sensors,
            StaminaManager staminaManager, EnemyMouth enemyMouth)
        {
            this.mover = mover;
            this.rotator = rotator;
            this.sensors = sensors;
            this.staminaManager = staminaManager;
            this.enemyMouth = enemyMouth;
        }
        public void Reset() => targetMovingTracker.Reset();
        public void SetTurnFinisher(TurnFinisher value) => turnFinisher = staminaManager.turnFinisher = value;
        public void SetTargetMovingTracker(TargetMovingTracker value) => targetMovingTracker = value;
        
        public void OnMoving(object sender, MovingEventArgs e)
        {
            if (!rotator.TryToRotateRightOrLeft(e.Destination))
                rotator.TryToRotateRightOrLeft(targetMovingTracker.TargetMovingEventArgs.Destination);
        }
        public void OnPlayerActionCompleted(object sender, MovingEventArgs e)
        {
            if (sensors.Any(sensor => sensor.IsInSensitivityRange(e.Destination)))
                targetMovingTracker.DetectTarget(e);

            if(targetMovingTracker.IsTargetDetected)
                if (!targetMovingTracker.IsTargetOnPosition(mover.transform.position))
                {
                    if (turnFinisher.TryToFinish(targetMovingTracker.TargetMovingEventArgs))
                        if (!staminaManager.TryToUse())
                            staminaManager.TryToRestore();
                }
                else
                    targetMovingTracker.OverlookTarget();
        }
        public void OnTurnFinished(object sender, MovingEventArgs e)
        {
            if (!mover.TryToMakeClosestMoveToTarget(e.Destination))
                targetMovingTracker.TryToOverlookTarget();
            else
                targetMovingTracker.StopOverlookingTarget();
        }
        
        public void OnTargetDetected(object sender, MovingEventArgs e) => enemyMouth.StartSalivateMore();
        public void OnTargetOverlooked(object sender, MovingEventArgs e) => enemyMouth.FinishSalivateMore();
        #endregion
    }
}