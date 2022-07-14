using Roguelike.Core.Information;
using Roguelike.Core.Services.Input;
using UnityEngine;
using Zenject;

namespace Roguelike.Core.Movers
{
    public class PlayerMover : MonoBehaviour
    {
        #region Fields
        [SerializeField] float movementStep = 1f;
        
        DungeonInfo dungeonInfo;
        IMovementInputService movementInputService;
        #endregion

        #region Methods
        [Inject]
        public void Construct(IMovementInputService movementInputService, DungeonInfo dungeonInfo)
        {
            this.movementInputService = movementInputService;
            this.dungeonInfo = dungeonInfo;
        }

        void OnEnable() => movementInputService.Moving += TryToMove;
        void OnDisable() => movementInputService.Moving -= TryToMove;
        
        void TryToMove(Vector3 translation)
        {
            if (dungeonInfo.IsPlayerMovePossible(transform.position + translation))
                Move(translation);
        }
        void Move(Vector3 translation) => transform.Translate(translation * movementStep);
        #endregion
    }
}