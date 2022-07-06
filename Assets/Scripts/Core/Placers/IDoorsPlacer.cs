using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Core.Placers
{
    public interface IDoorsPlacer
    {
        List<Transform>[] Place(List<Transform>[] doorMarkersByRoom);
    }
}