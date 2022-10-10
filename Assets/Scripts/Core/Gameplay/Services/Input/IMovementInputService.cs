using System;
using Roguelike.Core.Information;

namespace Roguelike.Core.Gameplay.Services.Input
{
    public interface IMovementInputService
    {
        event EventHandler<MovingEventArgs> Moving;
    }
}