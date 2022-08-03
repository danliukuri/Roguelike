using System.Collections.Generic;
using Roguelike.Core.Entities;
using Roguelike.Core.Information;
using UnityEngine;

namespace Roguelike.Core.Placers
{
    public interface IRoomsPlacer
    {
        List<Room> Place(LevelSettings levelSettings, Vector3 firstRoomPosition = default);
    }
}