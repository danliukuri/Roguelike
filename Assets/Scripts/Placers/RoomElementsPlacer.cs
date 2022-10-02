using System.Collections.Generic;
using System.Linq;
using Roguelike.Core.Factories;
using Roguelike.Core.Information;
using Roguelike.Core.Placers;
using Roguelike.Utilities.Extensions;
using UnityEngine;

namespace Roguelike.Placers
{
    public class RoomElementsPlacer : IRoomElementsPlacer
    {
        #region Fields
        const int MinNumberOfElementsToPlace = 1;
        readonly IGameObjectFactory gameObjectFactory;
        readonly Transform parent;
        readonly DungeonInfo dungeonInfo;
        #endregion
        
        #region Methods
        public RoomElementsPlacer(IGameObjectFactory gameObjectFactory, DungeonInfo dungeonInfo,
            Transform parent = default)
        {
            this.gameObjectFactory = gameObjectFactory;
            this.parent = parent;
            this.dungeonInfo = dungeonInfo;
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
        public List<Transform>[] PlaceRandom(List<Transform>[] elementMarkersByRoom,
            int numberOfElements = MinNumberOfElementsToPlace)
        {
            List<(Transform Marker, int RoomIndex)> activeElementMarkersByRoom = elementMarkersByRoom
                .SelectMany((markers, roomIndex) => markers
                    .Where(marker => marker.gameObject.activeSelf)
                    .Select(marker => (marker, roomIndex)))
                .ToList();
            
            List<Transform> placedElementMarkers = new List<Transform>(numberOfElements);
            List<Transform>[] elementsByRoom = new List<Transform>[elementMarkersByRoom.Length];
            for (int i = 0; i < numberOfElements; i++)
            {
                int elementMarkerIndex = activeElementMarkersByRoom.RandomIndex();
                
                (Transform elementMarker, int roomIndex) = activeElementMarkersByRoom.ElementAt(elementMarkerIndex);
                placedElementMarkers.Add(elementMarker);
                (elementsByRoom[roomIndex] ??= new List<Transform>()).Add(PlaceElement(elementMarker));
                
                activeElementMarkersByRoom.RemoveAt(elementMarkerIndex);
            }
            
            DeactivateAllMarkersExceptPlacedElementOnes(elementMarkersByRoom, placedElementMarkers);
            return elementsByRoom;
        }
        Transform PlaceElement(Transform elementMarker)
        {
            GameObject elementGameObject = gameObjectFactory.GetGameObject();
            
            Transform elementTransform = elementGameObject.transform;
            elementTransform.position = elementMarker.position;
            elementTransform.SetParent(parent == default ? elementMarker : parent);
            
            elementGameObject.SetActive(true);
            DeactivateEmptyMarkersOnPosition(elementMarker.position);
            return elementTransform;
        }
        
        void DeactivateEmptyMarkersOnPosition(Vector3 markerPosition)
        {
            int markerRoomIndex = dungeonInfo.GetRoomIndex(markerPosition);
            dungeonInfo.Rooms[markerRoomIndex].DeactivateEmptyMarkersOnPosition(markerPosition);
        }
        static void DeactivateAllMarkersExceptPlacedElementOnes(IEnumerable<List<Transform>> elementMarkersByRoom,
            IReadOnlyCollection<Transform> placedElementMarkers)
        {
            foreach (List<Transform> elementMarkers in elementMarkersByRoom)
                foreach (Transform elementMarker in elementMarkers.Except(placedElementMarkers))
                    elementMarker.gameObject.SetActive(false);
        }
        #endregion
    }
}