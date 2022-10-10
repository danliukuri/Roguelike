using Roguelike.Core.Setup.Placing;
using UnityEngine;

namespace Roguelike.Setup.Placing
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