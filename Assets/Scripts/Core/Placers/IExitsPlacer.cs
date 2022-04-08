using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Core.Placers
{
    public interface IExitsPlacer
    {
        void Place(List<Transform> exitsMarkers);
    }
}