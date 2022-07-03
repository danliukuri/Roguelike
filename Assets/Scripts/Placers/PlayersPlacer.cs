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
        public void Place(List<Transform> playerMarkers)
        {
            Transform playerMarker = playerMarkers.Where(m => m.gameObject.activeSelf).ToArray().Random();

            GameObject playerGameObject = playerFactory.GetPlayer();
            Transform playerTransform = playerGameObject.transform;

            playerTransform.position = playerMarker.position;
            playerTransform.SetParent(playerMarker);

            playerGameObject.SetActive(true);
        }
        #endregion
    }
}