using IoContainer.Abstract;

namespace IoContainer.Concrete
{
    internal class ServiceDescriptor : IService
    {
        public object Implementation { get; set; }
        public Type ImplementationType { get; set; }
        public Type? SourceType { get; set; }

        public ServiceDescriptor(Type implementationType, Type? sourceType = null)
        {
            ImplementationType = implementationType;

            if (sourceType != null)
            {
                SourceType = sourceType;
            }
        }

    }
}