namespace Roguelike.Core.Setup.Configuration
{
    public interface IConfigurator<T>
    {
        T Configure(T objectToConfigure);
    }
}