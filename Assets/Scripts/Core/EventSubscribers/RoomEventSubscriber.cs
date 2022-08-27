using Roguelike.Core.Entities;
using Roguelike.Core.EventHandlers;
using Roguelike.Core.Services.Controllers;
using UnityEngine;
using Zenject;

namespace Roguelike.Core.EventSubscribers
{
    public class RoomEventSubscriber : MonoBehaviour
    {
        #region Fields
        RoomEventHandler roomEventHandler;
        RoomBackgroundController backgroundController;
        Room room;
        #endregion
        
        #region Methods
        [Inject]
        void Construct(RoomEventHandler roomEventHandler, RoomBackgroundController backgroundController)
        {
            this.roomEventHandler = roomEventHandler;
            this.backgroundController = this.roomEventHandler.BackgroundController = backgroundController;
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