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
            if (Descriptors == null)
            {
                Descriptors = new List<ServiceDescriptor>();
            }
        }
        public TSource GetServiceAsSingleton<TSource>() where TSource : class
        {
            var descriptor = Descriptors.SingleOrDefault(descriptor => descriptor.ImplementationType == typeof(TSource) || descriptor.SourceType == typeof(TSource));
            var descriptorImpParamaterTypes = descriptor.ImplementationType.GetConstructors()[0].GetParameters().Select(param=>param.ParameterType).ToArray();
            object[] descriptorImpParameterObject = null;

            if (descriptorImpParamaterTypes.Length != 0)
            {
                descriptorImpParameterObject = new object[descriptorImpParamaterTypes.Length];

                for (int i = 0; i < descriptorImpParamaterTypes.Length; i++)
                {
                    var instance = GetServiceBase(descriptorImpParamaterTypes[i]);
                    descriptorImpParameterObject[i] = instance;
                }
                return (TSource)GetServiceBase(typeof(TSource),descriptorImpParameterObject);
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
        //This method only fill Implementation. It'll not produce instance as transient
        private object GetServiceBase(Type SourceType, object[]? param = null)
        {
            var proxyFactory = AutoProxyFactory.GetFactory();
            var descriptor = Descriptors.SingleOrDefault(descriptor => descriptor.SourceType == SourceType || descriptor.ImplementationType == SourceType);
            //var Methods = descriptor.ImplementationType.GetMethods().Where(method=>method.DeclaringType == descriptor.ImplementationType);
            if (descriptor.Implementation == null)
            {
                if (param != null)
                {
                    //AutoProxy'll integrate from here
                    descriptor.Implementation = Activator.CreateInstance(descriptor.ImplementationType, param);
                    var proxyObject = proxyFactory.CreateProxy(descriptor.Implementation,descriptor.SourceType);
                    //it'll return proxy object as descriptor.Implementation
                    return proxyObject;
                }
                else
                {
                    descriptor.Implementation = Activator.CreateInstance(descriptor.ImplementationType);
                    var proxyObject = proxyFactory.CreateProxy(descriptor.Implementation,descriptor.SourceType);
                    return proxyObject;
                }

            }
            else
            {
                return descriptor.Implementation;
            }
        }




    }
}