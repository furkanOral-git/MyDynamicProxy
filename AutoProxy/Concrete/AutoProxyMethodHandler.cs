using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoProxy.Concrete
{
    public class AutoProxyMethodHandler
    {
        private static AutoProxyMethodHandler _instance;

        private AutoProxyMethodHandler()
        {
            
        }
        //Every virtual proxy method call this method at runtime. So we can control to all method's behaviour and runtime args value from here
        //Moreover we can use to interceptor method with business logic at the same time
        public void Handle(object[] argsObject)
        {
            
        }
        public static AutoProxyMethodHandler GetHandler()
        {
            if (_instance == null)
            {
                _instance = new AutoProxyMethodHandler();
            }
            return _instance;
        }

    }
}