using Roguelike.Core.Movers;
using Roguelike.Core.Openers;
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
        IPicker keyPicker;
        IOpener doorOpener;
        
        IMovementInputService movementInputService;
        #endregion

        #region Methods
        [Inject]
        void Construct(IMovementInputService movementInputService, EntityMover mover, IPicker keyPicker,
            IOpener doorOpener)
        {
            this.movementInputService = movementInputService;
            this.mover = mover;
            this.keyPicker = keyPicker;
            this.doorOpener = doorOpener;
        }

        void OnEnable()
        {
            movementInputService.Moving += mover.TryToMove;
            mover.MovingToWall += mover.TryToMoveToWall;
            mover.MovingToKey += keyPicker.TryToPickUp;
            mover.MovingToDoor += doorOpener.TryToOpen;
        }
        void OnDisable()
        {
            movementInputService.Moving -= mover.TryToMove;
            mover.MovingToWall -= mover.TryToMoveToWall;
            mover.MovingToKey -= keyPicker.TryToPickUp;
            mover.MovingToDoor -= doorOpener.TryToOpen;
        }
        #endregion
    }
}