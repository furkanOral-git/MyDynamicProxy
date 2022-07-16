using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoProxy.Concrete;

namespace AutoProxy.Abstract
{
    public interface IAutoProxyFactory 
    {
        public object GetProxy(object implementation, Type SourceType);
    }
}