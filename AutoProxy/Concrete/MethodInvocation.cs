using System.Reflection;
using AutoProxy.Abstract;

namespace AutoProxy.Concrete
{
    public class MethodInvocation : AbstractMethodInvocation
    {
        public MethodInvocation(MethodInfo method, object declaringObject) : base(method, declaringObject)
        {
            
        }
    }
}