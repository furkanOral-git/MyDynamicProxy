using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoProxy.Abstract
{
    public interface IMethodInvocation
    {
        void Process(object[]? args);
    }
}