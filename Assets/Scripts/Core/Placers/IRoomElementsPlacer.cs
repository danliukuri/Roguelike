using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Core.Placers
{
    public interface IRoomElementsPlacer
    {
        List<Transform>[] PlaceAll(List<Transform>[] elementMarkersByRoom);
        List<Transform>[] PlaceRandom(List<Transform>[] elementMarkersByRoom, int numberOfElements = 1);
    }
}