using Roguelike.Core.Entities;
using Roguelike.Core.Factories;
using Roguelike.Utilities;
using Roguelike.Utilities.Extensions;
using Roguelike.Utilities.Pools;
using UnityEngine;

namespace Roguelike.Factories
{
    public class RoomFactory : MonoBehaviour, IRoomFactory
    {
        #region Fields
        [SerializeField] ObjectsPool roomsPool;
        #endregion

        #region Methods
        public GameObject GetRoom()
        {
            GameObject roomGameObject = roomsPool.GetFreeObject();
            Room room = roomGameObject.GetComponent<Room>();
            Transform roomTransform = roomGameObject.transform;

            roomTransform.RotateRandomNumberOfTimesByRightAngle(roomTransform.forward,
                room.SavePassagesDirectionsOnRotationToRight);
            room.Initialize();

            return roomGameObject;
        }
        #endregion
    }
}