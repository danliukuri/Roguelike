using UnityEngine;

namespace Roguelike.Core.Gameplay.Interaction.Pickers
{
    public interface IPicker
    {
        bool TryToPickUp(Transform item);
    }
}