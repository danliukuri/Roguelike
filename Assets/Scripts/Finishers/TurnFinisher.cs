using System;
using Roguelike.Core.Finishers;
using Roguelike.Core.Information;

namespace Roguelike.Finishers
{
    public class TurnFinisher : IFinisher<MovingEventArgs>
    {
        #region Properties
        public int NumberOfFreeActions { get; set; }
        public int NumberOfFreeActionsPerTurn { get; set; }
        #endregion
        
        #region Fields
        public event EventHandler<MovingEventArgs> Finished;
        
        const int NumberOfFreeActionsToFinishTurn = 0;
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