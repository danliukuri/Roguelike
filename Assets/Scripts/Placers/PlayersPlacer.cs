using System.Collections.Generic;
using System.Linq;
using Roguelike.Core.Factories;
using Roguelike.Core.Placers;
using Roguelike.Utilities.Extensions;
using UnityEngine;

namespace Roguelike.Placers
{
    public class PlayersPlacer : IPlayersPlacer
    {
        #region Fields
        readonly IPlayerFactory playerFactory;
        #endregion

        #region Methods
        public PlayersPlacer(IPlayerFactory playerFactory) => this.playerFactory = playerFactory;
        public List<Transform>[] Place(List<Transform>[] playerMarkersByRoom)
        {
            List<Transform>[] playersByRoom = new List<Transform>[playerMarkersByRoom.Length];
            for (int i = 0; i < playerMarkersByRoom.Length; i++)
                playersByRoom[i] = new List<Transform>();

            (Transform playerMarker, int roomIndex) = GetRandomMarker(playerMarkersByRoom); 
            
            GameObject playerGameObject = playerFactory.GetPlayer();
            Transform playerTransform = playerGameObject.transform;
            playersByRoom[roomIndex].Add(playerTransform);
            
            playerTransform.position = playerMarker.position;
            playerTransform.SetParent(playerMarker);
            
            playerGameObject.SetActive(true);
            
            return playersByRoom;
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