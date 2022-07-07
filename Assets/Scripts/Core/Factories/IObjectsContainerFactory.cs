using UnityEngine;

namespace Roguelike.Core.Factories
{
    public interface IObjectsContainerFactory
    {
        GameObject GetContainer(string name);
    }
}