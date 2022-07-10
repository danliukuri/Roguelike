using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Core.Placers
{
    public interface IRoomElementsPlacer
    {
        List<Transform>[] PlaceAll(List<Transform>[] elementMarkersByRoom);
        List<Transform>[] PlaceOneRandom(List<Transform>[] elementMarkersByRoom);
    }
}