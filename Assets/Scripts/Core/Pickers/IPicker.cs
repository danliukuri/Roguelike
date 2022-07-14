using UnityEngine;

namespace Roguelike.Core.Pickers
{
    public interface IPicker
    {
        bool TryToPickUp(Transform item);
    }
}