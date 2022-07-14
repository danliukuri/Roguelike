using Roguelike.Core.Movers;
using Roguelike.Core.Pickers;
using Roguelike.Core.Services.Input;
using UnityEngine;
using Zenject;

namespace Roguelike.Core.EventSubscribers
{
    public class PlayerEventSubscriber : MonoBehaviour
    {
        #region Fields
        EntityMover mover;
        ItemsPicker itemsPicker;
        
        IMovementInputService movementInputService;
        #endregion

        #region Methods
        [Inject]
        void Construct(IMovementInputService movementInputService, EntityMover mover, ItemsPicker itemsPicker)
        {
            this.movementInputService = movementInputService;
            this.mover = mover;
            this.itemsPicker = itemsPicker;
        }

        void OnEnable()
        {
            movementInputService.Moving += mover.TryToMove;
            mover.MovingToWall += mover.TryToMoveToWall;
            mover.MovingToKey += itemsPicker.TryToPickUpKey;
        }
        void OnDisable()
        {
            movementInputService.Moving -= mover.TryToMove;
            mover.MovingToWall -= mover.TryToMoveToWall;
            mover.MovingToKey -= itemsPicker.TryToPickUpKey;
        }
        #endregion
    }
}