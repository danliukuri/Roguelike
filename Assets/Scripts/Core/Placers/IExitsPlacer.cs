using System.Collections.Generic;
using UnityEngine;

namespace Roguelike.Core.Placers
{
    public interface IExitsPlacer
    {
        List<Transform>[] Place(List<Transform>[] exitMarkersByRoom);
    }
}