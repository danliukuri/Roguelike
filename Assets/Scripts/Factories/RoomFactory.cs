using Roguelike.Core.Factories;
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
            // Make random rotation
            return roomGameObject;
        }
        #endregion
    }
}