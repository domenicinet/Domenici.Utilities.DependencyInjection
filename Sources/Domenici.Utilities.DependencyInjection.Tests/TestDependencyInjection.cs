using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domenici.Utilities.DependencyInjection.Plugins;
using System.Diagnostics;
using Domenici.Utilities.DependencyInjection.Plugins.Generics;
using System.Text;

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

        [TestMethod]
        public void TestInstantiateObject()
        {
            string signatures = @"[
                                        {
                                            'key' : 'StringBuilder',
                                            'signature' : 'mscorlib',
                                            'namespace' : 'System.Text.StringBuilder'
                                        },
                                        {
                                            'key' : 'Random', 
                                            'signature' : 'mscorlib',
                                            'namespace' : 'System.Random'
                                        }
                                    ]";

            StringBuilder sb = null;
            Random rnd = null;

            using (PluginFactory pf = new PluginFactory(signatures))
            {
                sb = (StringBuilder)pf.Create("StringBuilder");
                rnd = (Random)pf.Create("Random");
            }

            Assert.IsNotNull(sb);
            Assert.IsNotNull(rnd);

            Assert.IsInstanceOfType(sb, typeof(StringBuilder));
            Assert.IsInstanceOfType(rnd, typeof(Random));
        }

        [TestMethod]
        public void TestGenericsInstantiateObject()
        {
            string signatures = @"[
                                        {
                                            'key' : 'StringBuilder',
                                            'signature' : 'mscorlib',
                                            'namespace' : 'System.Text.StringBuilder'
                                        },
                                        {
                                            'key' : 'Random', 
                                            'signature' : 'mscorlib',
                                            'namespace' : 'System.Random'
                                        }
                                    ]";

            StringBuilder sb = null;

            using (PluginFactory<StringBuilder> pf = new PluginFactory<StringBuilder>(signatures))
            {
                sb = pf.Create("StringBuilder");
            }

            Assert.IsNotNull(sb);
            Assert.IsInstanceOfType(sb, typeof(StringBuilder));
        }
    }
}
