using System;
using Roguelike.Core.Gameplay.Events.Handling;
using Roguelike.Core.Gameplay.Transformation.Moving;
using Zenject;

namespace Roguelike.Core.Gameplay.Events.Subscribing
{
    public class LevelEventSubscriber : IInitializable, IDisposable
    {
        #region Fields
        readonly LevelEventHandler levelEventHandler;
        readonly EntityMover playerMover;
        #endregion
        
        #region Methods
        public LevelEventSubscriber(LevelEventHandler levelEventHandler, EntityMover playerMover)
        {
            this.levelEventHandler = levelEventHandler;
            this.playerMover = playerMover;
        }
        
        public void Initialize() => playerMover.MovingToExit += levelEventHandler.OnPlayerIsMovingToExit;
        public void Dispose() => playerMover.MovingToExit -= levelEventHandler.OnPlayerIsMovingToExit;
        #endregion
    }
}