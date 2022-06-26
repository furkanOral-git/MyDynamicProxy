using IoContainer.Abstract;

namespace IoContainer.Concrete
{
    internal class ServiceContainer : IServiceContainer
    {
        private List<ServiceDescriptor> Descriptors { get; set; }

        public ServiceContainer()
        {
            if (Descriptors == null)
            {
                Descriptors = new List<ServiceDescriptor>();
            }
        }
        public TService GetServiceAsSingeleton<TService>() where TService : class, new()
        {
            throw new NotImplementedException();
        }

        public TSource GetServiceAsSingleton<TSource>() where TSource : class
        {
            throw new NotImplementedException();
        }
        internal List<ServiceDescriptor> GetDescriptors()
        {
            return Descriptors;
        }
        



    }
}