using Roguelike.Core.Information;
using Roguelike.Setup.Loading;

namespace Roguelike.Core.Gameplay.Events.Handling
{
    public class LevelEventHandler
    {
        #region Fields
        readonly LevelLoader levelLoader;
        #endregion
        
        #region Methods
        public LevelEventHandler(LevelLoader levelLoader) => this.levelLoader = levelLoader;
        
        public void OnPlayerIsMovingToExit(object sender, MovingEventArgs e) => levelLoader.LoadNextLevel();
        #endregion
    }
}