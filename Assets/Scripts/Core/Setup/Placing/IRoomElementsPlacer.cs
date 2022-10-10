using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Core.Setup.Placing
{
    public interface IRoomElementsPlacer
    {
        List<Transform>[] PlaceAll(List<Transform>[] elementMarkersByRoom);
        List<Transform>[] PlaceRandom(List<Transform>[] elementMarkersByRoom, int numberOfElements = 1);
    }
}