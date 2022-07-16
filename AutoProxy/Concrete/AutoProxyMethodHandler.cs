using System.Diagnostics;
using AutoProxy.Abstract;

namespace AutoProxy.Concrete
{
    public class AutoProxyMethodHandler
    {
        private static AutoProxyMethodHandler _instance;
        private static IDescriptorContainer _container;

        private AutoProxyMethodHandler()
        {
            _container = (IDescriptorContainer)AutoProxyFactory.GetFactory();
        }
        //Every virtual proxy method call this method at runtime. So we can control to all method's behaviour and runtime args value from here
        //Moreover we can use to interceptor method with business logic at the same time
        public void Handle(params object[] argsObject)
        {
            StackTrace st = new StackTrace(true);
            var frame = st.GetFrame(1);
            var proxyMethod = frame.GetMethod();
            var proxyType = proxyMethod.DeclaringType;
            var descriptor = _container.GetProxyDescriptor(proxyType);
            var mainMethodInvocation = descriptor.Invocations.SingleOrDefault(inv => inv.Method.Name == proxyMethod.Name);
            mainMethodInvocation.MethodParameterObjects = argsObject;
            mainMethodInvocation.Process();

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