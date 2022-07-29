using System.Collections.Generic;
using Roguelike.Core.Entities;
using UnityEngine;

namespace Roguelike.Core.Placers
{
    public interface IRoomsPlacer
    {
        List<Room> Place(int count, Vector3 firstRoomPosition = default);
    }
}