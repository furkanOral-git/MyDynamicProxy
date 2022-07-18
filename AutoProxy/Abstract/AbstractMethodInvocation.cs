using System.Reflection;


namespace AutoProxy.Abstract
{
    public abstract class AbstractMethodInvocation : IMethodInvocation
    {
        public MethodInfo Method { get; init; }
        public object DeclaringObject { get; init; }
        public bool IsInvoked { get; set; }
        
        public AbstractMethodInvocation(MethodInfo method, object declaringObject)
        {
            Method = method;
            DeclaringObject = declaringObject;
        }
        public void Process(object[]? args)
        {

            try
            {
                if (args != null)
                {

                     Method.Invoke(DeclaringObject, args);
                }
                else
                {
                     Method.Invoke(DeclaringObject, null);
                }
            }
            catch (System.Exception e)
            {
                throw e;
            }

        }
    }
}