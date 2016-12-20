using System;

namespace Domenici.Utilities.DependencyInjection.Plugins
{
    public class PluginFactoryException : Exception
    {
        public PluginFactoryException(string message) : base(message) { }
        public PluginFactoryException(string message, Exception innerException) : base(message, innerException) { }
    }
}