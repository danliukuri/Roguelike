using Roguelike.Core.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Core.Placers
{
    public interface IRoomsPlacer
    {
        List<Room> Place(Vector3 startPosition, int count, Transform parent);
    }
}