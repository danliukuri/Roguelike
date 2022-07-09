using Roguelike.Core.Entities;
using Roguelike.Core.Factories;
using Roguelike.Utilities.Extensions;
using Roguelike.Utilities.Pools;
using UnityEngine;

namespace Roguelike.Factories
{
    public class RoomFactory : MonoBehaviour, IGameObjectFactory
    {
        #region Fields
        [SerializeField] ObjectsPool roomsPool;
        #endregion

        #region Methods
        public GameObject GetGameObject()
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