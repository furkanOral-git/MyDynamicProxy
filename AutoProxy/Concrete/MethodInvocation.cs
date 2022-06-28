using System.Reflection;
using AutoProxy.Abstract;

namespace AutoProxy.Concrete
{
    public class MethodInvocation : IMethodInvocation
    {
        public MethodInfo Method { get; set; }
        public object DeclaringObject { get; set; }
        public object[] MethodParameterObjects { get; set; }

        public MethodInvocation(MethodInfo method,object declaringObject,object[]? parameterObjects = null)
        {
            Method = method;
            DeclaringObject = declaringObject;
            MethodParameterObjects = parameterObjects;
        }
        public object? Process()
        {
            try
            {
                if(MethodParameterObjects != null){

                    return Method.Invoke(DeclaringObject, MethodParameterObjects);
                }
                else
                {
                    return Method.Invoke(DeclaringObject,null);
                }
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }
    }
}