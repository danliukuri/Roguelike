using System;
using Roguelike.Core.Gameplay.Tracking.Finishing;
using Roguelike.Core.Information;

namespace Roguelike.Gameplay.Tracking.Finishing
{
    public class TurnFinisher : IFinisher<MovingEventArgs>
    {
        #region Properties
        public int NumberOfFreeActions { get; set; }
        public int NumberOfFreeActionsPerTurn { get; set; }
        #endregion
        
        #region Events
        public event EventHandler<MovingEventArgs> Finished;
        #endregion
        
        #region Fields
        const int NumberOfFreeActionsToFinishTurn = default;
        #endregion
        
        #region Methods
        public TurnFinisher(int initialNumberOfFreeActionsPerTurn = NumberOfFreeActionsToFinishTurn) =>
            NumberOfFreeActionsPerTurn = initialNumberOfFreeActionsPerTurn;
        
        public bool TryToFinish(MovingEventArgs movingEventArgs)
        {
            bool canFinishTurn = CanFinish();
            if (canFinishTurn)
            {
                Finished?.Invoke(this, movingEventArgs);
                NumberOfFreeActions = NumberOfFreeActionsPerTurn;
            }
            else
                NumberOfFreeActions--;
            return canFinishTurn;
        }
        bool CanFinish() => NumberOfFreeActions <= NumberOfFreeActionsToFinishTurn;
        #endregion
    }
}