using System;
using System.Collections.Generic;
using System.Linq;
using Roguelike.Core.Information;
using UnityEngine;
using Zenject;

namespace Roguelike.Core.Movers
{
    public class EntityMover : MonoBehaviour
    {
        #region Properties
        public int RoomIndex { get; private set; }
        #endregion
        
        #region Fields
        [SerializeField] float movementStep = 1f;
        [SerializeField] bool canMoveToWall;
        
        public delegate bool MovingToElementEventHandler(Transform element);
        public event MovingToElementEventHandler MovingToWall;
        public event MovingToElementEventHandler MovingToKey;
        public event Action<int> RoomIndexChanged;
        
        DungeonInfo dungeonInfo;
        #endregion

        #region Methods
        [Inject]
        public void Construct(DungeonInfo dungeonInfo) => this.dungeonInfo = dungeonInfo;
        
        public void TryToMove(Vector3 translation)
        {
            if (IsMovePossible(transform.position + translation))
                Move(translation);
        }
        public bool TryToMoveToWall(Transform wall) => canMoveToWall;
        void Move(Vector3 translation) => transform.Translate(translation * movementStep);
        
        bool IsMovePossible(Vector3 destination)
        {
            int roomIndex = dungeonInfo.GetRoomIndex(destination);
            if (RoomIndex != roomIndex)
            {
                RoomIndex = roomIndex;
                RoomIndexChanged?.Invoke(roomIndex);
            }
            
            var roomElementsInfo = new (List<Transform>[] ElementsByRoom, 
                MovingToElementEventHandler MovingToElement)[] 
                { 
                    (dungeonInfo.KeysByRoom, MovingToKey),
                    (dungeonInfo.WallsByRoom, MovingToWall),
                };
            
            bool isMovePossible = true;
            foreach (var (elementsByRoom, movingToElement) in roomElementsInfo)
            {
                Transform elementWhichEntityIsMovingTo = elementsByRoom[roomIndex]?
                    .FirstOrDefault(element => element.position == destination);
                bool isElementAtDestination = elementWhichEntityIsMovingTo != default;
            
                if (movingToElement != default && isElementAtDestination) 
                    isMovePossible = movingToElement.Invoke(elementWhichEntityIsMovingTo);
                
                if(isElementAtDestination)
                    break;
            }
            return isMovePossible;
        }
        #endregion
    }
}