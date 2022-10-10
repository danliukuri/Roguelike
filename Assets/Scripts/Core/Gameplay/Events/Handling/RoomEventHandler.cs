using Roguelike.Core.Gameplay.Transformation.Activation;

namespace Roguelike.Core.Gameplay.Events.Handling
{
    public class RoomEventHandler
    {
        #region Properties
        public RoomPassagesBackgroundActivator PassagesBackgroundActivator { get; set; }
        #endregion
        
        #region Methods
        public void OnPassageOpened(int directionIndex) => PassagesBackgroundActivator.Enable(directionIndex);
        public void OnRotatedToRight() => PassagesBackgroundActivator.ChangeOrientationToEast();
        #endregion
    }
}