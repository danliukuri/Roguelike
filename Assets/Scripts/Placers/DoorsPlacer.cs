using Roguelike.Core.Factories;
using Roguelike.Core.Placers;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Placers
{
    public class DoorsPlacer : IDoorsPlacer
    {
        #region Fields
        readonly IGameObjectFactory doorFactory;
        #endregion

        #region Methods
        public DoorsPlacer(IGameObjectFactory doorFactory) => this.doorFactory = doorFactory;
        public List<Transform>[] Place(List<Transform>[] doorMarkersByRoom)
        {
            List<Transform>[] doorsByRoom = new List<Transform>[doorMarkersByRoom.Length];
            for (int i = 0; i < doorMarkersByRoom.Length; i++)
            {
                doorsByRoom[i] = new List<Transform>();
                foreach (Transform doorMarker in doorMarkersByRoom[i])
                    if (doorMarker.gameObject.activeSelf)
                    {
                        GameObject doorGameObject = doorFactory.GetGameObject();
                        Transform doorTransform = doorGameObject.transform;
                        doorsByRoom[i].Add(doorTransform);
                        
                        doorTransform.position = doorMarker.position;
                        doorTransform.SetParent(doorMarker);

                        doorGameObject.SetActive(true);
                    }
            }
            return doorsByRoom;
        }
        #endregion
    }
}
