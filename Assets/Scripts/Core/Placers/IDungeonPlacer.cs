using UnityEngine;

namespace Roguelike.Core.Placers
{
    public interface IDungeonPlacer
    {
        void Place(Vector3 firstRoomPosition, int numberOfRooms, Transform parent, int numberOfKeys);
    }
}