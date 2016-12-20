using System;
using Domenici.Utilities.DependencyInjection.Plugins.Generics;

namespace BusinessLogic
{
    public class ServiceFactory<T> : PluginFactory<T>
    {
        public ServiceFactory(string assemblySignatures) : base(assemblySignatures) { }

        public T CreateService(string key)
        {
            // Return instance
            return base.Create(key);
        }
    }
}
