using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoProxy
{
    public abstract class MethodInterceptor : AbstractInterceptorBase
    {
        protected abstract void OnBefore(IMethodInvocation method);
        protected abstract void OnSucces(IMethodInvocation method);
        protected abstract void OnException(IMethodInvocation method, Exception e);
        protected abstract void OnAfter(IMethodInvocation method);

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
            if (HasResultObject == true)
            {
                return result;
            }
            else
            {
                return default;
            }
            OnAfter(method);
        }
    }
}