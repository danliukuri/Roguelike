using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Core.Placers
{
    public interface IWallsPlacer
    {
        void Place(List<Transform> wallsMarkers);
    }
}