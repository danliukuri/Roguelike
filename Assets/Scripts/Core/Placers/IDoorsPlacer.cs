using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Core.Placers
{
    public interface IDoorsPlacer
    {
        void Place(List<Transform> doorsMarkers);
    }
}