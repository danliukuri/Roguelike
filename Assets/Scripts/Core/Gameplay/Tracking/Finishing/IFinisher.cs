using System;

namespace Roguelike.Core.Gameplay.Tracking.Finishing
{
    public interface IFinisher<T>
    {
        #region Properties
        event EventHandler<T> Finished;
        #endregion
        
        #region Methods
        bool TryToFinish(T value);
        #endregion
    }
}