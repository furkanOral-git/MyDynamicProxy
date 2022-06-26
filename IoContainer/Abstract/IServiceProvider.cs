using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoContainer.Abstract
{
    public interface IServiceProvider
    {
        public IServiceContainer InitContainer();
        public void RegisterAsSingleton<TSource, TService>() where TService : TSource, new();
        public void RegisterAsSingeleton<TService>() where TService : class, new();
    }
}