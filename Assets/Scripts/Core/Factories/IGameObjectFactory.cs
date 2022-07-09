using UnityEngine;

namespace Roguelike.Core.Factories
{
    public interface IGameObjectFactory
    {
        GameObject GetGameObject();
    }
}