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
        const string playerParentName = "Players";
        
        readonly IPlayerFactory playerFactory;
        readonly IObjectsContainerFactory containerFactory;
        #endregion

        #region Methods
        public PlayersPlacer(IPlayerFactory playerFactory, IObjectsContainerFactory containerFactory)
        {
            this.playerFactory = playerFactory;
            this.containerFactory = containerFactory;
        }
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
            SetPlayerParent(playerTransform);
            
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
        void SetPlayerParent(Transform playerTransform)
        {
            GameObject playersContainer = containerFactory.GetContainer(playerParentName);
            playerTransform.SetParent(playersContainer.transform, false);
            playersContainer.SetActive(true);
        }
        #endregion
    }
}