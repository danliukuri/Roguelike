using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Core.Placers
{
    public interface IPlayersPlacer
    {
        void Place(List<Transform> playersMarkers);
    }
}