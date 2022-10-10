using System.Collections.Generic;
using System.Linq;
using Roguelike.Core.Gameplay.Interaction.Sensation;
using Roguelike.Core.Information;
using Roguelike.Utilities.Finding.Plotting;
using UnityEngine;
using Zenject;

namespace Roguelike.Gameplay.Interaction.Sensation
{
    public class VisionSensor : Sensor
    {
        #region Fields
        DungeonInfo dungeonInfo;
        ILinePlotter lineOfSightPlotter;
        #endregion
        
        #region Methods
        [Inject]
        public void Construct(DungeonInfo dungeonInfo)
        {
            this.dungeonInfo = dungeonInfo;
            lineOfSightPlotter = new LinePlottingAdapter(CanSeeThroughElement);
        }
        
        public override bool IsInSensitivityRange(Vector3 targetPosition)
        {
            Vector3 startPosition = transform.position;
            return (targetPosition - startPosition).magnitude <= sensitivityRange &&
                   lineOfSightPlotter.CanLineBePlotted(startPosition, targetPosition);
        }
        public bool CanSeeThroughElement(Vector3 elementPosition)
        {
            List<Transform>[][] elementsThroughWhichEntityCannotSee =
            { 
                dungeonInfo.WallsByRoom,
                dungeonInfo.DoorsByRoom
            };
            int elementRoomIndex = dungeonInfo.GetRoomIndex(elementPosition);
            return elementsThroughWhichEntityCannotSee.All(elementsByRoom =>
                elementsByRoom[elementRoomIndex].All(element => element.position != elementPosition));
        }
        #endregion
    }
}