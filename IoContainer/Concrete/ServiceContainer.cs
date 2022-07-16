using IoContainer.Abstract;
using AutoProxy;
using System.Reflection;
using System;

namespace IoContainer.Concrete
{
    internal class ServiceContainer : IServiceContainer
    {
        private List<ServiceDescriptor> Descriptors { get; set; }

        public ServiceContainer()
        {
            Descriptors = new List<ServiceDescriptor>();
        }
        public TSource GetServiceAsSingleton<TSource>() where TSource : class
        {
            var descriptor = Descriptors.SingleOrDefault(descriptor => descriptor.ImplementationType == typeof(TSource) || descriptor.SourceType == typeof(TSource));
            var descriptorImpParamaterTypes = descriptor.ImplementationType.GetConstructors()[0].GetParameters().Select(param => param.ParameterType).ToArray();
            object[] descriptorImpParameterObject = null;

            if (descriptorImpParamaterTypes.Length != 0)
            {
                descriptorImpParameterObject = new object[descriptorImpParamaterTypes.Length];

                for (int i = 0; i < descriptorImpParamaterTypes.Length; i++)
                {
                    var instance = GetServiceBase(descriptorImpParamaterTypes[i]);
                    descriptorImpParameterObject[i] = instance;
                }
                return (TSource)GetServiceBase(typeof(TSource), descriptorImpParameterObject);
            }
            else
            {
                return (TSource)GetServiceBase(typeof(TSource));
            }

        }
        internal List<ServiceDescriptor> GetDescriptors()
        {
            return Descriptors;
        }
        //This method only return implementation or gets a proxy object. It'll not produce instance as transient
        private object GetServiceBase(Type SourceType, object[]? param = null)
        {
            var descriptor = Descriptors.SingleOrDefault(descriptor => descriptor.SourceType == SourceType || descriptor.ImplementationType == SourceType);
            
            if (descriptor.Implementation == null)
            {
                if (param != null)
                {
                    
                    object returnObject = null;
                    descriptor.Implementation = Activator.CreateInstance(descriptor.ImplementationType, param);
                    //AutoProxy integrated with proxyUsage property
                    if(descriptor.ProxyUsage)
                    {
                        var proxyFactory = AutoProxyFactory.GetFactory();
                        returnObject = proxyFactory.GetProxy(descriptor.Implementation,descriptor.SourceType);
                    }
                    else
                    {
                        returnObject = descriptor.Implementation;
                    }
                     
                    return returnObject;
                }
                else
                {
                    object returnObject = null;
                    descriptor.Implementation = Activator.CreateInstance(descriptor.ImplementationType);

                    if(descriptor.ProxyUsage)
                    {
                        var proxyFactory = AutoProxyFactory.GetFactory();
                        returnObject = proxyFactory.GetProxy(descriptor.Implementation, SourceType);
                    }
                    else
                    {
                        returnObject = descriptor.Implementation;
                    }
                    return returnObject;
                }

            }
            else
            {
                if(descriptor.ProxyUsage)
                {
                    var proxyFactory = AutoProxyFactory.GetFactory();
                    return proxyFactory.GetProxy(descriptor.Implementation,descriptor.SourceType);
                }
                else
                {
                    return descriptor.Implementation;
                }
            }
        }




    }
}