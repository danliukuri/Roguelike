namespace Roguelike.Core.Configurators
{
    public interface IConfigurator<T>
    {
        T Configure(T objectToConfigure);
    }
}