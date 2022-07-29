using Roguelike.Core.Information;

namespace Roguelike.Core.Placers
{
    public interface IDungeonPlacer
    {
        void Place(LevelSettings levelSettings);
    }
}