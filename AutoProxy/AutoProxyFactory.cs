using System.Reflection.Emit;
using System.Reflection;
using AutoProxy.Concrete;
using AutoProxy.Abstract;

namespace AutoProxy
{
    public class AutoProxyFactory : IAutoProxyFactory, IDescriptorContainer
    {
        private static AutoProxyFactory _factory;
        private AssemblyBuilder _assemblyBuilder;
        private ModuleBuilder _moduleBuilder;
        private List<ProxyDescriptor> _proxies;
        private AutoProxyFactory()
        {
            if (_assemblyBuilder == null)
            {
                AssemblyName assemblyName = new AssemblyName("VirtualAssembly");
                _assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);

            }
            if (_moduleBuilder == null && _assemblyBuilder != null)
            {

                _moduleBuilder = _assemblyBuilder.DefineDynamicModule("VirtualModule");
            }
            _proxies = new List<ProxyDescriptor>();

        }
        public static AutoProxyFactory GetFactory()
        {

            if (_factory == null)
            {
                _factory = new AutoProxyFactory();
            }
            return _factory;
        }

        public ProxyDescriptor GetProxyDescriptor(Type proxyType)
        {
            return _proxies.FirstOrDefault(proxy => proxy.ProxyType == proxyType);
        }
        public object GetProxy(object implementation, Type SourceType)
        {
            var implementationType = implementation.GetType();
            var constantHandleObject = AutoProxyMethodHandler.GetHandler();

            if (_proxies.Any(proxy => proxy.ImplementationType == implementationType || proxy.SourceType == SourceType) == false)
            {
                var proxyType = CreateProxyType(implementationType, SourceType);
                var descriptor = CreateDescriptor(SourceType, proxyType, implementation);
                descriptor.ProxyObject = Activator.CreateInstance(proxyType, constantHandleObject);
                return descriptor.ProxyObject;
            }
            else
            {
                var descriptor = _proxies.FirstOrDefault(proxy => proxy.ImplementationType == implementationType);

                if (descriptor.ProxyObject == null)
                {
                    descriptor.ProxyObject = Activator.CreateInstance(descriptor.ProxyType, constantHandleObject);
                }
                return descriptor.ProxyObject;
            }
        }
        private Type CreateProxyType(Type implementationType, Type SourceType)
        {
            var MainClassName = implementationType.Name;
            //Type, Implementation, Field ,Constructor describing
            var typeBuilder = _moduleBuilder.DefineType($"{MainClassName}Proxy"
            , TypeAttributes.Public | TypeAttributes.AutoClass | TypeAttributes.AnsiClass | TypeAttributes.BeforeFieldInit
            , typeof(Object));

            typeBuilder.AddInterfaceImplementation(SourceType);

            var field = typeBuilder.DefineField("_methodHandler", typeof(AutoProxyMethodHandler), FieldAttributes.Private);

            var constructor = typeBuilder.DefineConstructor(
                MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName
                , CallingConventions.HasThis
                , new Type[] { typeof(AutoProxyMethodHandler) });

            constructor.DefineParameter(1, ParameterAttributes.None, "methodHandler");
            constructor.SetImplementationFlags(MethodImplAttributes.IL);
            AutoProxyEmitter.EmitConstructor(constructor, field);

            var handleMethod = typeof(AutoProxyMethodHandler).GetMethod("Handle");
            var methods = SourceType.GetMethods();
            for (int i = 0; i < methods.Length; i++)
            {
                var definedMethod = ImplementMethod(typeBuilder, methods[i]);
                AutoProxyEmitter.EmitMethod(definedMethod, field, methods[i], handleMethod);
            }

            var proxyTypeConcrete = typeBuilder.CreateType();

            //Create Type object from Typebuilder, this is final operation for generate a proxy class
            return proxyTypeConcrete;

        }
        private MethodBuilder ImplementMethod(TypeBuilder builder, MethodInfo actualMethod)
        {
            var definedMethod = builder.DefineMethod(actualMethod.Name
                , MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Virtual
                , CallingConventions.HasThis
                , actualMethod.ReturnType
                , actualMethod.GetParameters().Select(p => p.ParameterType).ToArray());

            definedMethod.SetImplementationFlags(MethodImplAttributes.IL);
            return definedMethod;
        }

        private ProxyDescriptor CreateDescriptor(Type sourceType, Type proxyType, object implementationObject)
        {
            Type ImpType = implementationObject.GetType();

            List<MethodInvocation> invs = new List<MethodInvocation>();
            var methods = sourceType.GetMethods();
            for (int i = 0; i < methods.Length; i++)
            {
                var inv = new MethodInvocation(methods[i], implementationObject);
                invs.Add(inv);
            }
            var descriptor = new ProxyDescriptor(sourceType, implementationObject, proxyType, invs);
            _proxies.Add(descriptor);
            return descriptor;


        }
    }
}