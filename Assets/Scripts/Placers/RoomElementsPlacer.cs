using System.Collections.Generic;
using System.Linq;
using Roguelike.Core.Factories;
using Roguelike.Core.Placers;
using Roguelike.Utilities.Extensions;
using UnityEngine;

namespace Roguelike.Placers
{
    public class RoomElementsPlacer : IRoomElementsPlacer
    {
        #region Fields
        readonly IGameObjectFactory gameObjectFactory;
        readonly Transform parent;
        #endregion

        #region Methods
        public RoomElementsPlacer(IGameObjectFactory gameObjectFactory, Transform parent = default)
        {
            this.gameObjectFactory = gameObjectFactory;
            this.parent = parent;
        }

        public List<Transform>[] PlaceAll(List<Transform>[] elementMarkersByRoom)
        {
            List<Transform>[] elementsByRoom = elementMarkersByRoom
                .Select(elementMarkers => elementMarkers
                    .Where(elementMarker => elementMarker.gameObject.activeSelf)
                    .Select(PlaceElement)
                    .ToList())
                .ToArray();
            return elementsByRoom;
        }
        public List<Transform>[] PlaceRandom(List<Transform>[] elementMarkersByRoom, int numberOfElements = 1)
        {
            List<(Transform Marker, int RoomIndex)> activeElementMarkersByRoom = elementMarkersByRoom
                .SelectMany((markers, roomIndex) => markers
                    .Where(marker => marker.gameObject.activeSelf)
                    .Select(marker => (marker, roomIndex)))
                .ToList();
            
            List<Transform>[] elementsByRoom = new List<Transform>[elementMarkersByRoom.Length];
            for (int i = 0; i < numberOfElements; i++)
            {
                int elementMarkerIndex = activeElementMarkersByRoom.RandomIndex();
                
                (Transform elementMarker, int roomIndex) = activeElementMarkersByRoom.ElementAt(elementMarkerIndex);
                (elementsByRoom[roomIndex] ??= new List<Transform>()).Add(PlaceElement(elementMarker));
                
                activeElementMarkersByRoom.RemoveAt(elementMarkerIndex);
            }
            return elementsByRoom;
        }
        
        Transform PlaceElement(Transform elementMarker)
        {
            GameObject elementGameObject = gameObjectFactory.GetGameObject();
            elementGameObject.SetActive(true);
            
            Transform elementTransform = elementGameObject.transform;
            elementTransform.position = elementMarker.position;
            elementTransform.SetParent(parent == default ? elementMarker : parent);
            return elementTransform;
        }
        #endregion
    }
}