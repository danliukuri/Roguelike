using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Core.Placers
{
    public interface IWallsPlacer
    {
        List<Transform>[] Place(List<Transform>[] wallMarkersByRoom);
    }
}