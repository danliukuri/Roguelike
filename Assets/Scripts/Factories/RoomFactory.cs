using Roguelike.Core.Entities;
using Roguelike.Core.Factories;
using Roguelike.Utilities.Extensions;
using Roguelike.Utilities.Pools;
using UnityEngine;

namespace Roguelike.Factories
{
    public class RoomFactory : MonoBehaviour, IRoomFactory
    {
        #region Properties
        public Room Sample { get; private set; }
        #endregion
        
        #region Fields
        [SerializeField] ObjectsPool roomsPool;
        #endregion
        
        #region Methods
        void Start() => InitializeSample();
        void InitializeSample() => Sample = roomsPool.GetFreeObject().GetComponent<Room>();

        public Room GetRoom()
        {
            GameObject roomGameObject = roomsPool.GetFreeObject();
            roomGameObject.SetActive(true);
            
            Room room = roomGameObject.GetComponent<Room>();
            Transform roomTransform = roomGameObject.transform;
            
            roomTransform.RotateRandomNumberOfTimesByRightAngle(roomTransform.forward, room.RotateToRight);
            return room;
        }
        #endregion
    }
}