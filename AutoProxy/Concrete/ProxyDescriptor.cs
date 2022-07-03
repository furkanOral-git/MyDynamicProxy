using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoProxy.Concrete
{
    public class ProxyDescriptor
    {
        public Type SourceType { get; init; }

        public List<MethodInvocation> Invocations { get; set; }

        public ProxyDescriptor(Type sourceType, List<MethodInvocation> invocations)
        {
            SourceType = sourceType;
            Invocations = invocations;
        }
    }
}