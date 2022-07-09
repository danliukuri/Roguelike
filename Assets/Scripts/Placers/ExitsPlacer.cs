using Roguelike.Core.Factories;
using Roguelike.Core.Placers;
using Roguelike.Utilities.Extensions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Roguelike.Placers
{
    public class ExitsPlacer : IExitsPlacer
    {
        #region Fields
        readonly IGameObjectFactory exitFactory;
        #endregion

        #region Methods
        public ExitsPlacer(IGameObjectFactory exitFactory) => this.exitFactory = exitFactory;
        public List<Transform>[] Place(List<Transform>[] exitMarkersByRoom)
        {
            List<Transform>[] exitsByRoom = new List<Transform>[exitMarkersByRoom.Length];
            for (int i = 0; i < exitMarkersByRoom.Length; i++)
                exitsByRoom[i] = new List<Transform>();
            
            (Transform exitMarker, int roomIndex) = GetRandomMarker(exitMarkersByRoom); 
            
            GameObject exitGameObject = exitFactory.GetGameObject();
            Transform exitTransform = exitGameObject.transform;
            exitsByRoom[roomIndex].Add(exitTransform);
            
            exitTransform.position = exitMarker.position;
            exitTransform.SetParent(exitMarker);

            exitGameObject.SetActive(true);

            return exitsByRoom;
        }
        static (Transform marker, int roomIndex) GetRandomMarker(IEnumerable<List<Transform>> markersByRoom)
        {
            (IEnumerable<Transform> pickedMarkers, int pickedRoomIndex) = markersByRoom
                .Select((markers, roomIndex) => 
                    (markers: markers.Where(marker => marker.gameObject.activeSelf), roomIndex))
                .Where(activeMarkersByRoom => activeMarkersByRoom.markers.Any())
                .ToArray().Random();
            return (pickedMarkers.ToArray().Random(), pickedRoomIndex);
        }
        #endregion
    }
}
