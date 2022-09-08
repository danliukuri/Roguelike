using UnityEngine;

namespace Roguelike.Core.Placers
{
    public interface IGameObjectPlacer
    {
        GameObject Place(GameObject objectToPlace);
    }
}