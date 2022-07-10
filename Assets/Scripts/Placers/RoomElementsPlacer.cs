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
        #endregion

        #region Methods
        public RoomElementsPlacer(IGameObjectFactory gameObjectFactory) => this.gameObjectFactory = gameObjectFactory;
        
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
        public List<Transform>[] PlaceOneRandom(List<Transform>[] elementMarkersByRoom)
        {
            List<Transform>[] elementsByRoom = new List<Transform>[elementMarkersByRoom.Length];
            for (int i = 0; i < elementMarkersByRoom.Length; i++)
                elementsByRoom[i] = new List<Transform>();

            (Transform elementMarker, int roomIndex) = GetRandomMarker(elementMarkersByRoom);
            elementsByRoom[roomIndex].Add(PlaceElement(elementMarker));
            
            return elementsByRoom;
        }
        Transform PlaceElement(Transform elementMarker)
        {
            GameObject elementGameObject = gameObjectFactory.GetGameObject();
            elementGameObject.SetActive(true);
            
            Transform elementTransform = elementGameObject.transform;
            elementTransform.position = elementMarker.position;
            SetParent(elementTransform, elementMarker);
            return elementTransform;
        }
        
        static (Transform Marker, int RoomIndex) GetRandomMarker(IEnumerable<IEnumerable<Transform>> markersByRoom)
        {
            (IEnumerable<Transform> pickedMarkers, int pickedRoomIndex) = markersByRoom
                .Select((markers, roomIndex) => 
                    (Markers: markers.Where(marker => marker.gameObject.activeSelf), roomIndex))
                .Where(activeMarkersByRoom => activeMarkersByRoom.Markers.Any())
                .ToArray().Random();
            return (pickedMarkers.ToArray().Random(), pickedRoomIndex);
        }
        protected virtual void SetParent(Transform elementTransform, Transform parent) => 
            elementTransform.SetParent(parent);
        #endregion
    }
}