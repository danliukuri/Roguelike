using UnityEngine;

namespace Roguelike.Core.Factories
{
    public interface IPlayerFactory
    {
        GameObject GetPlayer();
    }
}