using System.Reflection.Emit;
using System.Reflection;
using AutoProxy.Concrete;
using AutoProxy.Tools;

namespace AutoProxy
{
    public class AutoProxyFactory
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
                AssemblyBuilder assembly = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);

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
        public object CreateProxy(object implementation, Type SourceType)
        {
            var MainClassName = implementation.GetType().Name;
            //Type, Implementation, Field ,Constructor describing
            var typeBuilder = _moduleBuilder.DefineType($"{MainClassName}Proxy");
            typeBuilder.AddInterfaceImplementation(SourceType);
            var field = typeBuilder.DefineField("_methodHandler", typeof(AutoProxyMethodHandler), FieldAttributes.Private);
            var constructor = typeBuilder.DefineConstructor(MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName, CallingConventions.Any, null);
            var methods = ImplementMethods(SourceType, typeBuilder);
            AutoProxyEmitter.EmitType(typeBuilder, constructor, field, methods);

            //Create Type object from Typebuilder, this is final operation for generate a proxy class
            var proxyTypeConcrete = typeBuilder.CreateType();
            //Creates descriptor object for proxy
            CreateDescriptor(SourceType, implementation);
            //Singleton or transient
            return Activator.CreateInstance(proxyTypeConcrete);
        }
        private MethodBuilder[] ImplementMethods(Type SourceType, TypeBuilder builder)
        {
            var methods = SourceType.GetMethods();
            MethodBuilder[] proxyMethods = new MethodBuilder[methods.Length];

            for (int i = 0; i < methods.Length; i++)
            {
                var definedMethod = builder.DefineMethod(methods[i].Name, MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Virtual);
                var parameters = methods[i].GetParameters();
                if (parameters != null)
                {
                    for (int index = 0; i < parameters.Length; i++)
                    {
                        definedMethod.DefineParameter(index, ParameterAttributes.In, parameters[i].Name);
                    }
                }
                proxyMethods[i] = definedMethod;

            }
            return proxyMethods;
        }
        private void CreateDescriptor(Type sourceType, object implementationObject)
        {

            if (_proxies.Any(proxy => proxy.SourceType != sourceType))
            {
                List<MethodInvocation> invs = new List<MethodInvocation>();
                Type ImpType = implementationObject.GetType();
                var methods = ImpType.GetMethods().Where(method => method.DeclaringType == ImpType).ToArray();
                for (int i = 0; i < methods.Length; i++)
                {
                    if (invs.Any(inv => inv.Method != methods[i] && inv.DeclaringObject.GetType() != ImpType))
                    {
                        var inv = new MethodInvocation(methods[i], implementationObject);
                        invs.Add(inv);
                    }
                }
                var descriptor = new ProxyDescriptor(sourceType, invs);
                _proxies.Add(descriptor);

            }
        }
    }
}