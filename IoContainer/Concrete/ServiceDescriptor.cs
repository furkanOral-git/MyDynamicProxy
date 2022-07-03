using AutoProxy.Abstract;
using AutoProxy.Concrete;
using IoContainer.Abstract;

namespace IoContainer.Concrete
{
    internal class ServiceDescriptor 
    {
        public object Implementation { get; set; }
        public Type ImplementationType { get; init; }
        public Type SourceType { get; init; }

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