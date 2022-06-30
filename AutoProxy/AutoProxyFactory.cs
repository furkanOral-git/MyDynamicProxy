using System.Reflection.Emit;
using System.Reflection;
using AutoProxy.Abstract;
using AutoProxy.Tools;

namespace AutoProxy
{
    public class AutoProxyFactory
    {
        private static AutoProxyFactory _factory;
        private AssemblyBuilder _assemblyBuilder;
        private ModuleBuilder _moduleBuilder;

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

        }
        public static AutoProxyFactory GetFactory()
        {

            if (_factory == null)
            {
                _factory = new AutoProxyFactory();
            }
            return _factory;
        }
        public object CreateProxy(object Implementation, Type SourceType)
        {
            string MainClassName = Implementation.GetType().Name;
            Type MainClassType = Implementation.GetType();

            //Type, Implementation, Field ,Constructor describing
            var typeBuilder = _moduleBuilder.DefineType($"{MainClassName}Proxy");
            typeBuilder.AddInterfaceImplementation(SourceType);
            var field = typeBuilder.DefineField("_methodHandler",typeof(AutoProxyMethodHandler),FieldAttributes.Private);
            var constructor = typeBuilder.DefineConstructor(MethodAttributes.Public|MethodAttributes.HideBySig|MethodAttributes.SpecialName|MethodAttributes.RTSpecialName,CallingConventions.Any,null);
            var methods = _factory.ImplementMethods(SourceType,typeBuilder);
            AutoProxyEmitter.EmitType(typeBuilder, constructor,field, methods);

            //Create Type object from Typebuilder, this is final operation for generate a proxy class
            var proxyTypeConcrete = typeBuilder.CreateType();
            //Singleton or transient
            return Activator.CreateInstance(proxyTypeConcrete);
        }
        private MethodBuilder[] ImplementMethods(Type InterfaceType, TypeBuilder builder)
        {
            var methods = InterfaceType.GetMethods();
            MethodBuilder[] proxyMethods = new MethodBuilder[methods.Length];

            for (int i = 0; i < methods.Length; i++)
            {
                var definedMethod = builder.DefineMethod(methods[i].Name, MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Virtual);
                var parameters = methods[i].GetParameters();
                if (parameters != null)
                {
                    for (int index = 0; i < parameters.Length; i++)
                    {
                        definedMethod.DefineParameter(index,ParameterAttributes.In,parameters[i].Name);
                    }
                }
                proxyMethods[i] = definedMethod;

            }
            return proxyMethods;
        }
    }
}