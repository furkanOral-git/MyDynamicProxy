using AutoProxy.Abstract;
using AutoProxy.Concrete;

namespace AutoProxy.Aspects
{
    //Aspects must be inherited from this class
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public abstract class MethodInterceptor : AbstractInterceptorBase
    {
        public int Priorty { get { return this.Priorty; } set => this.Priorty = value; }
        protected virtual void OnBefore(MethodInvocation method, object[]? args) { }
        protected virtual void OnSucces(MethodInvocation method, object[]? args) { }
        protected virtual void OnException(MethodInvocation method, Exception e) { }
        protected virtual void OnAfter(MethodInvocation method, object[]? args) { }

        protected static T SolveParameter<T>(object[] args, int index = -1)
        {
            try
            {
                var Args = args.ToArray();
                if (index == -1)
                {
                    return (T)Args.FirstOrDefault(arg => arg.GetType() == typeof(T));
                }
                else
                {
                    return (T)Args.ElementAtOrDefault(Index.FromStart(index));
                }
            }
            catch (ArgumentNullException e)
            {

                throw new Exception($"Has no parameter there type of {typeof(T)}");
            }


        }
        public override void Intercept(MethodInvocation method, object[]? args)
        {
            bool IsSuccess = true;

            OnBefore(method, args);
            try
            {
                if (!method.IsInvoked)
                {
                    method.Process(args);
                    method.IsInvoked = true;
                }

            }
            catch (System.Exception e)
            {
                IsSuccess = false;
                OnException(method, e);
                throw;
            }
            finally
            {
                if (IsSuccess)
                {
                    OnSucces(method, args);
                }

            }
            OnAfter(method, args);
            
        }
    }
}