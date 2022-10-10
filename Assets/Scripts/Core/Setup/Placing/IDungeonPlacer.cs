using Roguelike.Core.Information;

namespace Roguelike.Core.Setup.Placing
{
    public interface IDungeonPlacer
    {
        void Place(LevelSettings levelSettings);
    }
}