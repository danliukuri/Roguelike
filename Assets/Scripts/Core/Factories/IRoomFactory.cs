using Roguelike.Core.Entities;

namespace Roguelike.Core.Factories
{
    public interface IRoomFactory
    {
        #region Properties
        Room Sample { get; }
        #endregion
        
        #region Methods
        Room GetRoom();
        #endregion
    }
}