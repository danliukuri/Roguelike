using Roguelike.Core.Information;
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
        PlayerMover mover;
        ItemsPicker itemsPicker;

        DungeonInfo dungeonInfo;
        IMovementInputService movementInputService;
        #endregion

        #region Methods
        [Inject]
        void Construct(IMovementInputService movementInputService, DungeonInfo dungeonInfo, ItemsPicker itemsPicker)
        {
            this.movementInputService = movementInputService;
            this.dungeonInfo = dungeonInfo;
            this.itemsPicker = itemsPicker;
        }
        void Awake()
        {
            mover = GetComponent<PlayerMover>();
        }

        void OnEnable()
        {
            movementInputService.Moving += mover.TryToMove;
            dungeonInfo.PlayerToKeyMoving += itemsPicker.PickUpKey;
        }
        void OnDisable()
        {
            movementInputService.Moving -= mover.TryToMove;
            dungeonInfo.PlayerToKeyMoving -= itemsPicker.PickUpKey;
        }
        #endregion
    }
}