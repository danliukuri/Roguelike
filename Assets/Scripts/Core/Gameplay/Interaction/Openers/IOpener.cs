using UnityEngine;

namespace Roguelike.Core.Gameplay.Interaction.Openers
{
    public interface IOpener
    {
        bool TryToOpen(Transform thing);
    }
}