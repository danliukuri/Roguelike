using Roguelike.Core.Services.Controllers;

namespace Roguelike.Core.EventHandlers
{
    public class RoomEventHandler
    {
        #region Properties
        public RoomBackgroundController BackgroundController { get; set; }
        #endregion
        
        #region Methods
        public void OnPassageOpened(int directionIndex) => BackgroundController.EnablePassageBackground(directionIndex);
        public void OnRotatedToRight() => BackgroundController.ChangePassagesBackgroundOrientationToEast();
        #endregion
    }
}