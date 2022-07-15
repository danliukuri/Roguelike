using Roguelike.Core.EventHandlers;
using Roguelike.Core.Movers;
using Roguelike.Core.Services.Input;
using UnityEngine;
using Zenject;

namespace Roguelike.Core.EventSubscribers
{
    public class PlayerEventSubscriber : MonoBehaviour
    {
        #region Fields
        IMovementInputService movementInputService;
        EntityMover mover;
        
        PlayerEventHandler playerEventHandler;
        #endregion

        #region Methods
        [Inject]
        void Construct(IMovementInputService movementInputService, EntityMover mover,
            PlayerEventHandler playerEventHandler)
        {
            this.movementInputService = movementInputService;
            this.mover = mover;
            this.playerEventHandler = playerEventHandler;
        }

        void OnEnable()
        {
            movementInputService.Moving += playerEventHandler.OnMoving;
            mover.MovingToKey += playerEventHandler.OnMovingToKey;
            mover.MovingToDoor += playerEventHandler.OnMovingToDoor;
        }
        void OnDisable()
        {
            movementInputService.Moving -= playerEventHandler.OnMoving;
            mover.MovingToKey -= playerEventHandler.OnMovingToKey;
            mover.MovingToDoor -= playerEventHandler.OnMovingToDoor;
        }
        #endregion
    }
}