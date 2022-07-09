using System.Collections.Generic;
using Roguelike.Core.Factories;
using Roguelike.Core.Placers;
using UnityEngine;
using Zenject;

namespace Roguelike.Placers
{
    public class WallsPlacer : IRoomElementsPlacer
    {
        #region Fields
        readonly IGameObjectFactory wallFactory;
        #endregion

        #region Methods
        public WallsPlacer([Inject(Id = RoomElementType.Wall)] IGameObjectFactory wallFactory) =>
            this.wallFactory = wallFactory;
        public List<Transform>[] Place(List<Transform>[] wallMarkersByRoom)
        {
            List<Transform>[] wallsByRoom = new List<Transform>[wallMarkersByRoom.Length];
            for (int i = 0; i < wallMarkersByRoom.Length; i++)
            {
                wallsByRoom[i] = new List<Transform>();
                foreach (Transform wallMarker in wallMarkersByRoom[i])
                    if(wallMarker.gameObject.activeSelf)
                    {
                        GameObject wallGameObject = wallFactory.GetGameObject();
                        Transform wallTransform = wallGameObject.transform;
                        wallsByRoom[i].Add(wallTransform);
                            
                        wallTransform.position = wallMarker.position;
                        wallTransform.SetParent(wallMarker);

                        wallGameObject.SetActive(true);
                    }
            }
            return wallsByRoom;
        }
        #endregion
    }
}
