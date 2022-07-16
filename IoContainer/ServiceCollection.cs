using IoContainer.Abstract;
using IoContainer.Concrete;

namespace IoContainer
{
    public class ServiceCollection : IServiceCollection
    {
        private static IServiceCollection _instance;
        private ServiceContainer _containerInstance;

        private ServiceCollection()
        {

        }
        public IServiceContainer InitContainer()
        {
            if (this._containerInstance == null)
            {

                this._containerInstance = new ServiceContainer();
            }
            return this._containerInstance;
        }

        public static IServiceCollection InitServices()
        {
            if (_instance == null)
            {
                _instance = new ServiceCollection();
            }
            return _instance;
        }

        public void RegisterAsSingleton<TService>(bool proxyUsage) where TService : class, new()
        {
            var descriptors = _containerInstance.GetDescriptors();
            bool IsExist = descriptors.Any(descriptor => descriptor.ImplementationType == typeof(TService) && descriptor.SourceType == typeof(TService));

            if (IsExist)
            {
                System.Console.WriteLine("Service Provider : This service has already registered !");

            }
            else
            {
                descriptors.Add(new ServiceDescriptor(typeof(TService), proxyUsage));
            }
        }

        public void RegisterAsSingleton<TSource, TService>(bool proxyUsage) where TService : class, TSource
        {
            var descriptors = _containerInstance.GetDescriptors();
            bool IsExist = descriptors.Any(descriptor => descriptor.ImplementationType == typeof(TService) && descriptor.SourceType == typeof(TSource));

            if (IsExist)
            {
                System.Console.WriteLine("Service Provider : This service has already registered as singleton !");
            }
            else
            {
                descriptors.Add(new ServiceDescriptor(typeof(TService), typeof(TSource), proxyUsage));
            }
        }
    }
}