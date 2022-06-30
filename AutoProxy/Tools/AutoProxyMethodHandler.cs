using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoProxy.Tools
{
    public class AutoProxyMethodHandler
    {
        private static AutoProxyMethodHandler _instance;

        private AutoProxyMethodHandler()
        {
            
        }
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