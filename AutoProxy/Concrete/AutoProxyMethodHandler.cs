using System.Diagnostics;
using AutoProxy.Abstract;
using AutoProxy.Aspects;

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
        public void Handle(params object[] argsObjects)
        {
            StackTrace st = new StackTrace(true);
            var frame = st.GetFrame(1);
            var proxyMethod = frame.GetMethod();
            var proxyType = proxyMethod.DeclaringType;
            var descriptor = _container.GetProxyDescriptor(proxyType);
            var Invocation = descriptor.Invocations.SingleOrDefault(inv => inv.Method.Name == proxyMethod.Name);
            var actualMethod = descriptor.ImplementationType.GetMethod(Invocation.Method.Name);
            
            var attrs = actualMethod.GetCustomAttributesData().Where(attr => attr.AttributeType.IsAssignableTo(typeof(MethodInterceptor)));
            if (attrs != null)
            {
                List<MethodInterceptor> aspects = new List<MethodInterceptor>();
                foreach (var attr in attrs)
                {
                    var aspect = (MethodInterceptor)Activator.CreateInstance(attr.AttributeType, false);
                    aspects.Add(aspect);
                }
                aspects.OrderByDescending(asp=>asp.Priorty);
                
                foreach (var asp in aspects)
                {
                    asp.Intercept(Invocation,argsObjects);
                }
                //for reset invocation status info
                Invocation.IsInvoked = false;
            }
            else
            {
                Invocation.Process(argsObjects);
                //reset invocation status info
                Invocation.IsInvoked = false;
            }

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