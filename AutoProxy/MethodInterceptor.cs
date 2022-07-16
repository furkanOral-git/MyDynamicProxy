using AutoProxy.Abstract;

namespace AutoProxy
{
    public  class MethodInterceptor : AbstractInterceptorBase
    {
        protected virtual void OnBefore(IMethodInvocation method){}
        protected virtual void OnSucces(IMethodInvocation method){}
        protected virtual void OnException(IMethodInvocation method, Exception e){}
        protected virtual void OnAfter(IMethodInvocation method){}

        public override object? Intercept(IMethodInvocation method)
        {
            bool IsSuccess = true;
            bool HasResultObject = false;
            object result = null;
            
            OnBefore(method);
            try
            {
                result = method.Process();
                if (result != null)
                {
                    HasResultObject = true;
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
                    OnSucces(method);
                }

            }
            OnAfter(method);
            if (HasResultObject == true)
            {
                return result;
            }
            else
            {
                return default;
            }
            
        }
    }
}