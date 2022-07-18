using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoProxy.Concrete;

namespace AutoProxy.Abstract
{
    public abstract class AbstractInterceptorBase : Attribute
    {
        public abstract void Intercept(MethodInvocation method, object[]? args);


    }
}