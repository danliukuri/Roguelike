using Roguelike.Core.Factories;
using Roguelike.Core.Placers;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Placers
{
    public class DoorsPlacer : IDoorsPlacer
    {
        #region Fields
        readonly IDoorFactory doorFactory;
        #endregion

        #region Methods
        public DoorsPlacer(IDoorFactory doorFactory) => this.doorFactory = doorFactory;
        public void Place(List<Transform> doorsMarkers)
        {
            foreach (Transform doorMarker in doorsMarkers)
                if (doorMarker.gameObject.activeSelf)
                {
                    GameObject doorGameObject = doorFactory.GetDoor();
                    Transform doorTransform = doorGameObject.transform;

                    doorTransform.position = doorMarker.position;
                    doorTransform.SetParent(doorMarker);

                    doorGameObject.SetActive(true);
                }
        }
        #endregion
    }
}
