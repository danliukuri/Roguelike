using System;
using Roguelike.Core.Information;
using Roguelike.UI.EventHandlers;
using Zenject;

namespace Roguelike.UI.EventSubscribers
{
    public class GameplayMenuEventSubscriber : IInitializable, IDisposable
    {
        #region Fields
        readonly LevelSettings levelSettings;
        readonly GameplayMenuEventHandler eventHandler;
        #endregion
        
        #region Methods
        public GameplayMenuEventSubscriber(GameplayMenuEventHandler eventHandler, LevelSettings levelSettings)
        {
            this.eventHandler = eventHandler;
            this.levelSettings = levelSettings;
        }
        
        public void Initialize() => levelSettings.LevelNumberChanged += eventHandler.OnLevelNumberChanged;
        public void Dispose() => levelSettings.LevelNumberChanged -= eventHandler.OnLevelNumberChanged;
        #endregion
    }
}