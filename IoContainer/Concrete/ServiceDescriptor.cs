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
        public bool ProxyUsage { get; set; }
        public ServiceDescriptor(Type implementationType,Type sourceType,bool proxyUsage) 
        {
            ImplementationType = implementationType;
            SourceType = sourceType;
            ProxyUsage = proxyUsage;
        }
        
        // Don't repeat yourself
        public ServiceDescriptor(Type implementationType,bool proxyUsage) : this(implementationType,implementationType,proxyUsage)
        {
            
        }

    }
}