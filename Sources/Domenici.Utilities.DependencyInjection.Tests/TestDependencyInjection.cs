using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domenici.Utilities.DependencyInjection.Plugins;
using System.Diagnostics;
using Domenici.Utilities.DependencyInjection.Plugins.Generics;

namespace Domenici.Utilities.DependencyInjection.Tests
{
    [TestClass]
    public class TestDependencyInjection
    {
        #region Signatures
        string signatures = @"[
    {
    'key'       : 'Service1',
    'signature' : 'Service1, Version=1.0.0.0',
    'namespace' : 'Service1.Service'
    },
    {
    'key'       : 'Service2',
    'signature' : 'Service2, Version=1.0.0.0',
    'namespace' : 'Service2.Service'
    }
]";
        #endregion

        [TestMethod]
        public void TestConstructorSignaturesLoading()
        {
            PluginFactory factory = null;

            try
            {
                factory = new PluginFactory(signatures);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }

            Assert.IsNotNull(factory);
        }

        [TestMethod]
        public void TestGenericsConstructorSignaturesLoading()
        {
            PluginFactory<String> factory = null;

            try
            {
                factory = new PluginFactory<String>(signatures);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }

            Assert.IsNotNull(factory);
        }
    }
}
