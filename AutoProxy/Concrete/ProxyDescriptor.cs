using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoProxy.Concrete
{
    public class ProxyDescriptor 
    {
        public Type SourceType { get; init; }
        public Type ImplementationType { get; init; }
        public object Implementation { get; init; }
        public Type ProxyType { get; set; }
        public object ProxyObject { get; set; }
        public List<MethodInvocation> Invocations { get; init; }


        public ProxyDescriptor(Type sourceType, object implementation, Type proxyType, List<MethodInvocation> invocations)
        {
            SourceType = sourceType;
            Implementation = implementation;
            ImplementationType = implementation.GetType();
            Invocations = invocations;
            ProxyType = proxyType;
        }
    }
}