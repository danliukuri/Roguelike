using UnityEngine;

namespace Roguelike.Core.Openers
{
    public interface IOpener
    {
        bool TryToOpen(Transform thing);
    }
}