using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoProxy.Abstract
{
    public abstract class AbstractInterceptorBase
    {
        public abstract object? Intercept(IMethodInvocation method);

        
    }
}