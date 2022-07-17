using System;
using System.Collections.Generic;
using System.Linq;
using Roguelike.Core.Information;
using UnityEngine;
using Zenject;

namespace Roguelike.Core.Movers
{
    public class EntityMover : MonoBehaviour, IResettable
    {
        #region Properties
        public int RoomIndex { get; private set; }
        #endregion
        
        #region Fields
        [SerializeField] float movementStep = 1f;
        
        public event EventHandler<MovingEventArgs> MovingToWall;
        public event EventHandler<MovingEventArgs> MovingToKey;
        public event EventHandler<MovingEventArgs> MovingToDoor;
        public event Action<int> RoomIndexChanged;
        
        DungeonInfo dungeonInfo;
        #endregion

        #region Methods
        [Inject]
        public void Construct(DungeonInfo dungeonInfo) => this.dungeonInfo = dungeonInfo;
        public void Reset()
        {
            RoomIndex = default;
        }

        public void TryToMove(Vector3 translation)
        {
            if (IsMovePossible(transform.position + translation))
                Move(translation);
        }
        void Move(Vector3 translation) => transform.Translate(translation * movementStep);
        
        bool IsMovePossible(Vector3 destination)
        {
            int destinationRoomIndex = dungeonInfo.GetRoomIndex(destination);
            
            var roomElementsInfo = new (List<Transform>[] ElementsByRoom, 
                EventHandler<MovingEventArgs> MovingToElement)[] 
                { 
                    (dungeonInfo.KeysByRoom, MovingToKey),
                    (dungeonInfo.DoorsByRoom, MovingToDoor),
                    (dungeonInfo.WallsByRoom, MovingToWall),
                };
            var (elementWhichEntityIsMovingTo, movingToElement) = roomElementsInfo
                .Select(elementsInfo => (ElementAtDestination: elementsInfo.ElementsByRoom[destinationRoomIndex]?
                        .FirstOrDefault(element => element.position == destination), elementsInfo.MovingToElement))
                .FirstOrDefault(elementInfo => elementInfo.ElementAtDestination != default);
            
            bool isElementAtDestination = elementWhichEntityIsMovingTo != default;
            const bool canMoveToElements = false;
            const bool canMoveToEmptyCells = true;
            bool isMovePossible = canMoveToEmptyCells;
            
            if (isElementAtDestination)
                if (movingToElement != default)
                {
                    MovingEventArgs movingEventArgs = new MovingEventArgs
                    {
                        ElementWhichEntityIsMovingTo = elementWhichEntityIsMovingTo,
                        IsMovePossible = canMoveToElements,
                        Destination = destination
                    };
                    movingToElement.Invoke(this, movingEventArgs);
                    isMovePossible = movingEventArgs.IsMovePossible;
                }
                else
                    isMovePossible = canMoveToElements;

            if (isMovePossible && RoomIndex != destinationRoomIndex)
            {
                RoomIndex = destinationRoomIndex;
                RoomIndexChanged?.Invoke(destinationRoomIndex);
            }
            return isMovePossible;
        }
        #endregion
    }
}