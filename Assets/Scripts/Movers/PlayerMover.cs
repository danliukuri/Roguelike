using Roguelike.Core.Services.Input;
using UnityEngine;
using Zenject;

namespace Roguelike.Movers
{
    public class PlayerMover : MonoBehaviour
    {
        #region Fields
        [SerializeField] float movementStep = 1f;

        IMovementInputService movementInputService;
        #endregion

        #region Methods
        [Inject]
        public void Construct(IMovementInputService movementInputService)
        {
            this.movementInputService = movementInputService;
        }

        void OnEnable()
        {
            movementInputService.Moving += Move;
        }
        void OnDisable()
        {
            movementInputService.Moving -= Move;
        }

        void Move(Vector3 translation)
        {
            transform.Translate(translation * movementStep);
        }
        #endregion
    }
}