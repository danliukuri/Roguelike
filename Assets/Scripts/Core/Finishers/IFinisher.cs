using System;

namespace Roguelike.Core.Finishers
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