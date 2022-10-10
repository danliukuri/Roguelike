using Roguelike.Core.Entities;

namespace Roguelike.Core.Setup.Factories
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