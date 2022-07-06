using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Core.Placers
{
    public interface IPlayersPlacer
    {
        List<Transform>[] Place(List<Transform>[] playerMarkersByRoom);
    }
}