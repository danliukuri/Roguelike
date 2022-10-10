using Roguelike.Core.Entities;
using Roguelike.Core.Setup.Factories;
using Roguelike.Utilities.Extensions.Unity;
using UnityEngine;

namespace Roguelike.Setup.Factories
{
    public class RoomFactory : GameObjectFactory, IRoomFactory
    {
        #region Properties
        public Room Sample { get; private set; }
        #endregion
        
        #region Methods
        void Start() => InitializeSample();
        void InitializeSample() => Sample = base.GetGameObject().GetComponent<Room>();
        
        public Room GetRoom()
        {
            GameObject roomGameObject = base.GetGameObject();
            roomGameObject.SetActive(true);
            
            Room room = roomGameObject.GetComponent<Room>();
            Transform roomTransform = roomGameObject.transform;
            
            roomTransform.RotateRandomNumberOfTimesByRightAngle(roomTransform.forward, room.RotateToRight);
            return room;
        }
        #endregion
    }
}