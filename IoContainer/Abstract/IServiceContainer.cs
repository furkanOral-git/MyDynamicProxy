using IoContainer.Concrete;

namespace IoContainer.Abstract
{
    public interface IServiceContainer
    {
        public TService GetServiceAsSingeleton<TService>() where TService : class, new();
        public TSource GetServiceAsSingleton<TSource>() where TSource : class;
        
        
    }
}