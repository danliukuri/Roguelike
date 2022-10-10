using System.Linq;
using Roguelike.Core.Characteristics;
using Roguelike.Core.Entities;
using Roguelike.Core.Gameplay.Interaction.Sensation;
using Roguelike.Core.Gameplay.Spawning;
using Roguelike.Core.Gameplay.Tracking;
using Roguelike.Core.Gameplay.Transformation.Moving;
using Roguelike.Core.Gameplay.Transformation.Rotation;
using Roguelike.Core.Information;
using Roguelike.Gameplay.Tracking.Finishing;

namespace Roguelike.Core.Gameplay.Events.Handling
{
    public class EnemyEventHandler : IResettable
    {
        #region Fields
        readonly PathfindingEntityMover mover;
        readonly SpriteRotator rotator;
        readonly ISensor[] sensors;
        readonly Stamina stamina;
        readonly EnemySalivator salivator;
        TargetMovingTracker targetMovingTracker;
        TurnFinisher turnFinisher;
        #endregion
        
        #region Methods
        public EnemyEventHandler(PathfindingEntityMover mover, SpriteRotator rotator, ISensor[] sensors,
            Stamina stamina, EnemySalivator salivator)
        {
            this.mover = mover;
            this.rotator = rotator;
            this.sensors = sensors;
            this.stamina = stamina;
            this.salivator = salivator;
        }
        public void Reset() => targetMovingTracker.Reset();
        public void SetTurnFinisher(TurnFinisher value) => turnFinisher = stamina.TurnFinisher = value;
        public void SetTargetMovingTracker(TargetMovingTracker value) => targetMovingTracker = value;
        
        public void OnMoving(object sender, MovingEventArgs e)
        {
            if (!rotator.TryToRotateRightOrLeft(e.Destination))
                rotator.TryToRotateRightOrLeft(targetMovingTracker.TargetMovingEventArgs.Destination);
        }
        public void OnPlayerActionCompleted(object sender, MovingEventArgs e)
        {
            if (sensors.Any(sensor => sensor.IsInSensitivityRange(e.Destination)))
                targetMovingTracker.DetectTarget(e.ShallowCopy());
            
            if(targetMovingTracker.IsTargetDetected)
                if (!targetMovingTracker.IsTargetOnPosition(mover.transform.position))
                {
                    if (turnFinisher.TryToFinish(targetMovingTracker.TargetMovingEventArgs))
                        if (!stamina.TryToUse())
                            stamina.TryToRestore();
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
        
        public void OnTargetDetected(object sender, MovingEventArgs e)
        {
            salivator.FinishSalivate();
            salivator.StartSalivateMore();
        }
        public void OnTargetOverlooked(object sender, MovingEventArgs e)
        {
            salivator.FinishSalivateMore();
            salivator.StartSalivate();
        }
        #endregion
    }
}