namespace Roguelike.Utilities.Generic.Characteristics
{
    public interface IShallowCopyable<out T>
    {
        T ShallowCopy();
    }
}