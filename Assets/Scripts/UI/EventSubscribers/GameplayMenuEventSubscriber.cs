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
        readonly Inventory playerInventory;
        #endregion
        
        #region Methods
        public GameplayMenuEventSubscriber(GameplayMenuEventHandler eventHandler, LevelSettings levelSettings,
            Inventory playerInventory)
        {
            this.eventHandler = eventHandler;
            this.levelSettings = levelSettings;
            this.playerInventory = playerInventory;
        }
        
        public void Initialize()
        {
            levelSettings.LevelNumberChanged += eventHandler.OnLevelNumberChanged;
            playerInventory.NumberOfKeysChanged += eventHandler.OnPlayerNumberOfKeysChanged;
        }
        public void Dispose()
        {
            levelSettings.LevelNumberChanged -= eventHandler.OnLevelNumberChanged;
            playerInventory.NumberOfKeysChanged -= eventHandler.OnPlayerNumberOfKeysChanged;
        }
        #endregion
    }
}