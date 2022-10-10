using UnityEngine;

namespace Roguelike.Core.Setup.Placing
{
    public interface IGameObjectPlacer
    {
        GameObject Place(GameObject objectToPlace);
    }
}