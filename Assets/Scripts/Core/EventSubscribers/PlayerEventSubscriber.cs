using Roguelike.Core.Movers;
using Roguelike.Core.Pickers;
using Roguelike.Core.Services.Input;
using Roguelike.Pickers;
using UnityEngine;
using Zenject;

namespace Roguelike.Core.EventSubscribers
{
    public class PlayerEventSubscriber : MonoBehaviour
    {
        #region Fields
        EntityMover mover;
        IPicker keyPicker;
        
        IMovementInputService movementInputService;
        #endregion

        #region Methods
        [Inject]
        void Construct(IMovementInputService movementInputService, EntityMover mover, IPicker keyPicker)
        {
            this.movementInputService = movementInputService;
            this.mover = mover;
            this.keyPicker = keyPicker;
        }

        void OnEnable()
        {
            movementInputService.Moving += mover.TryToMove;
            mover.MovingToWall += mover.TryToMoveToWall;
            mover.MovingToKey += keyPicker.TryToPickUp;
        }
        void OnDisable()
        {
            movementInputService.Moving -= mover.TryToMove;
            mover.MovingToWall -= mover.TryToMoveToWall;
            mover.MovingToKey -= keyPicker.TryToPickUp;
        }
        #endregion
    }
}