using Roguelike.Core.Placers;
using UnityEngine;

namespace Roguelike.Placers
{
    public class ChildPlacer : MonoBehaviour, IGameObjectPlacer
    {
        #region Methods
        public GameObject Place(GameObject objectToPlace)
        {
            objectToPlace.transform.SetParent(transform);
            objectToPlace.transform.position = transform.position;
            return objectToPlace;
        }
        #endregion
    }
}