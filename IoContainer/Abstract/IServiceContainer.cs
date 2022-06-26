using IoContainer.Concrete;

namespace IoContainer.Abstract
{
    public interface IServiceContainer
    {
        public TSource GetServiceAsSingleton<TSource>() where TSource : class;
        
        
    }
}