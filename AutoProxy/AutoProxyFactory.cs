using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoProxy.Abstract;

namespace AutoProxy
{
    public class AutoProxyFactory
    {
        private static AutoProxyFactory _factory;

        private AutoProxyFactory()
        {

        }
        public static AutoProxyFactory GetFactory()
        {

            if (_factory == null)
            {
                _factory = new AutoProxyFactory();
            }
            return _factory;
        }
        public object CreateProxy(IService service)
        {
            return default;
        }
    }
}