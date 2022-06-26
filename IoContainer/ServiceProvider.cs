using IoContainer.Abstract;
using IoContainer.Concrete;

namespace IoContainer
{
    public class ServiceProvider
    {
        private static Abstract.IServiceProvider _instance;
        private ServiceContainer _containerInstance;

        private ServiceProvider()
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

        public static Abstract.IServiceProvider InitServices()
        {
            if (_instance == null)
            {
                _instance = (Abstract.IServiceProvider)new ServiceProvider();
            }
            return _instance;
        }
        public void RegisterAsSingleton<TSource, TService>() where TService : TSource, new()
        {
            var descriptors = _containerInstance.GetDescriptors();
            bool IsExist = descriptors.Any(descriptor => descriptor.ImplementationType == typeof(TService) && descriptor.SourceType == typeof(TSource));

            if (IsExist)
            {
                System.Console.WriteLine("Service Provider : This service has already registered !");
            }
            else
            {
                descriptors.Add(new ServiceDescriptor(typeof(TService), typeof(TSource)));
            }
        }
        public void RegisterAsSingeleton<TService>() where TService : class, new()
        {
            var descriptors = _containerInstance.GetDescriptors();
            bool IsExist = descriptors.Any(descriptor => descriptor.ImplementationType == typeof(TService));

            if (IsExist)
            {
                System.Console.WriteLine("Service Provider : This service has already registered !");
            }
            else
            {
                descriptors.Add(new ServiceDescriptor(typeof(TService)));
            }
        }
    }
}