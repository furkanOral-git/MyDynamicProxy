using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoContainer.Abstract
{
    public interface IServiceCollection
    {
        public IServiceContainer InitContainer();
        public void RegisterAsSingleton<TSource, TService>() where TService : class, TSource;
        public void RegisterAsSingeleton<TService>() where TService : class, new();
    }
}