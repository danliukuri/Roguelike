using UnityEngine;

namespace Roguelike.Core.Setup.Factories
{
    public interface IGameObjectFactory
    {
        GameObject GetGameObject();
    }
}