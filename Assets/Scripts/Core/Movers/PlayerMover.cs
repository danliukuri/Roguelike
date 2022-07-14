using Roguelike.Core.Information;
using UnityEngine;
using Zenject;

namespace Roguelike.Core.Movers
{
    public class PlayerMover : MonoBehaviour
    {
        #region Fields
        [SerializeField] float movementStep = 1f;
        
        DungeonInfo dungeonInfo;
        #endregion

        #region Methods
        [Inject]
        public void Construct(DungeonInfo dungeonInfo) => this.dungeonInfo = dungeonInfo;
        
        public void TryToMove(Vector3 translation)
        {
            if (dungeonInfo.IsPlayerMovePossible(transform.position + translation))
                Move(translation);
        }
        void Move(Vector3 translation) => transform.Translate(translation * movementStep);
        #endregion
    }
}