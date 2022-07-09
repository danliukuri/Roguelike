using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Core.Placers
{
    public interface IRoomElementsPlacer
    {
        List<Transform>[] Place(List<Transform>[] elementMarkersByRoom);
    }
}