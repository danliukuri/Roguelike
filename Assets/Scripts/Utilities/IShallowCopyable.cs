namespace Roguelike.Utilities
{
    public interface IShallowCopyable<out T>
    {
        T ShallowCopy();
    }
}