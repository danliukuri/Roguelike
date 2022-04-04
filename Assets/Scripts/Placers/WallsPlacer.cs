using Roguelike.Core.Factories;
using Roguelike.Core.Placers;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Placers
{
    public class WallsPlacer : IWallsPlacer
    {
        #region Fields
        readonly IWallFactory wallFactory;
        #endregion

        #region Methods
        public WallsPlacer(IWallFactory wallFactory) => this.wallFactory = wallFactory;
        public void Place(List<Transform> wallsMarkers)
        {
            foreach (Transform wallMarker in wallsMarkers)
                if(wallMarker.gameObject.activeSelf)
                {
                    GameObject wallGameObject = wallFactory.GetWall();
                    Transform wallTransform = wallGameObject.transform;
                    
                    wallTransform.position = wallMarker.position;
                    wallTransform.SetParent(wallMarker);

                    wallGameObject.SetActive(true);
                }
        }
        #endregion
    }
}
