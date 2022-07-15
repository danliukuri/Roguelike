using System;
using Roguelike.Core.Information;

namespace Roguelike.Core.Services.Input
{
    public interface IMovementInputService
    {
        event EventHandler<MovingEventArgs> Moving;
    }
}