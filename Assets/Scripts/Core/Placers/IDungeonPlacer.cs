using UnityEngine;

namespace Roguelike.Core.Placers
{
    public interface IDungeonPlacer
    {
        void Place(Vector3 firstRoomPosition, int roomsCount, Transform parent);
    }
}