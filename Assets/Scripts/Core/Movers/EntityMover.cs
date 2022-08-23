using System;
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
        [field: SerializeField] public float MovementStep { get; private set; } = 1f;
        #endregion
        
        #region Fields
        public event EventHandler<MovingEventArgs> MovingToWall;
        public event EventHandler<MovingEventArgs> MovingToKey;
        public event EventHandler<MovingEventArgs> MovingToDoor;
        public event EventHandler<MovingEventArgs> MovingToExit;
        public event EventHandler<MovingEventArgs> ActionCompleted;
        public event Action<int> RoomIndexChanged;
        
        DungeonInfo dungeonInfo;
        MovingInfo[] generalMovingInfo;
        #endregion
        
        #region Methods
        [Inject]
        public void Construct(DungeonInfo dungeonInfo) => this.dungeonInfo = dungeonInfo;
        void OnEnable()
        {
            if (dungeonInfo.Rooms != default) 
                RoomIndex = dungeonInfo.GetRoomIndex(transform.position);
        }
        public void Reset() => generalMovingInfo = default;
        
        void SetRoomIndexIfNew(int roomIndex)
        {
            if (RoomIndex != roomIndex)
            {
                dungeonInfo.ChangeRoomIndex(transform, RoomIndex, roomIndex);
                RoomIndex = roomIndex;
                RoomIndexChanged?.Invoke(roomIndex);
            }
        }
        void Move(Vector3 translation, MovingEventArgs movingArgs)
        {
            transform.Translate(translation * MovementStep);
            SetRoomIndexIfNew(movingArgs.ElementRoomIndex);
        }
        
        public bool TryToMove(Vector3 translation)
        {
            Vector3 destination = transform.position + translation;
            MovingInfo movingInfo = GetMovingInfo(destination);
            MovingEventArgs movingArgs = movingInfo.Args;
            
            if (!movingArgs.IsMovePossible)
                movingInfo.Event?.Invoke(this, movingArgs);
            
            if (movingArgs.IsMovePossible)
                Move(translation, movingArgs);
            else
                movingArgs.Destination = transform.position;
            ActionCompleted?.Invoke(this, movingArgs);
            
            return movingArgs.IsMovePossible;
        }
        public bool IsMovePossible(Vector3 destination, MovingInfo[] generalMovingInfo = default) =>
            GetMovingInfo(destination, generalMovingInfo).Args.IsMovePossible;
        
        MovingInfo GetMovingInfo(Vector3 destination, MovingInfo[] generalMovingInfo = default)
        {
            generalMovingInfo ??= GetGeneralMovingInfo();
            int destinationRoomIndex = dungeonInfo.GetRoomIndex(destination);

            MovingInfo movingInfo = generalMovingInfo.FirstOrDefault(elementInfo =>
                (elementInfo.Args.Element = elementInfo.ElementsByRoom[destinationRoomIndex]
                    ?.FirstOrDefault(element => element.position == destination)) != default);
            movingInfo ??= new MovingInfo { Args = new MovingEventArgs() };
            
            MovingEventArgs movingArgs = movingInfo.Args;
            movingArgs.ElementRoomIndex = destinationRoomIndex;
            movingArgs.IsMovePossible = movingArgs.Element == default;
            movingArgs.Destination = destination;
            
            return movingInfo;
        }
        MovingInfo[] GetGeneralMovingInfo() => generalMovingInfo ??= new[]
        {
            new MovingInfo { ElementsByRoom = dungeonInfo.KeysByRoom, Event = MovingToKey,
                Args = new MovingEventArgs() },
            new MovingInfo { ElementsByRoom = dungeonInfo.DoorsByRoom, Event = MovingToDoor,
                Args = new MovingEventArgs() },
            new MovingInfo { ElementsByRoom = dungeonInfo.ExitsByRoom, Event = MovingToExit,
                Args = new MovingEventArgs() },
            new MovingInfo { ElementsByRoom = dungeonInfo.WallsByRoom, Event = MovingToWall,
                Args = new MovingEventArgs() },
        };
        #endregion
    }
}