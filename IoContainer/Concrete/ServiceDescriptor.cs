using IoContainer.Abstract;

namespace IoContainer.Concrete
{
    internal class ServiceDescriptor : IService
    {
        public object Implementation { get; set; }
        public Type ImplementationType { get; set; }
        public Type? SourceType { get; set; }

        public ServiceDescriptor(Type implementationType,Type sourceType) 
        {
            ImplementationType = implementationType;
            SourceType = sourceType;
        }
        
        // Don't repeat yourself
        public ServiceDescriptor(Type implementationType) : this(implementationType,implementationType)
        {
            
        }

    }
}