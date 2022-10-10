using Roguelike.Core.Entities;
using Roguelike.Core.Gameplay.Events.Handling;
using Roguelike.Core.Gameplay.Transformation.Activation;
using UnityEngine;
using Zenject;

namespace Roguelike.Core.Gameplay.Events.Subscribing
{
    public class RoomEventSubscriber : MonoBehaviour
    {
        #region Fields
        RoomEventHandler roomEventHandler;
        RoomPassagesBackgroundActivator passagesBackgroundActivator;
        Room room;
        #endregion
        
        #region Methods
        [Inject]
        void Construct(RoomEventHandler roomEventHandler, RoomPassagesBackgroundActivator passagesBackgroundActivator)
        {
            this.roomEventHandler = roomEventHandler;
            this.passagesBackgroundActivator = this.roomEventHandler.PassagesBackgroundActivator = passagesBackgroundActivator;
            room = GetComponent<Room>();
        }
        
        void OnEnable()
        {
            room.PassageOpened += roomEventHandler.OnPassageOpened;
            room.RotatedToRight += roomEventHandler.OnRotatedToRight;
        }
        void OnDisable()
        {
            room.PassageOpened -= roomEventHandler.OnPassageOpened;
            room.RotatedToRight -= roomEventHandler.OnRotatedToRight;
        }
        #endregion
    }
}