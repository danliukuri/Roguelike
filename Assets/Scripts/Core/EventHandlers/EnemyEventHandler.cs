using Roguelike.Core.Information;
using Roguelike.Core.Movers;

namespace Roguelike.Core.EventHandlers
{
    public class EnemyEventHandler
    {
        #region Fields
        readonly PathfindingEntityMover mover;
        #endregion
        
        #region Methods
        public EnemyEventHandler(PathfindingEntityMover mover) => this.mover = mover;
        
        public void OnPlayerActionCompleted(object sender, MovingEventArgs e) =>
            mover.TryToMakeClosestMoveToTarget(e.Destination);
        #endregion
    }
}