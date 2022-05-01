using System;
using UnityEngine;

namespace Roguelike.Core.Services.Input
{
    public interface IMovementInputService
    {
        event Action<Vector3> Moving;
    }
}