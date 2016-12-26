using System;
using BusinessLogic;
using Settings = Domenici.Utilities.Configuration.SampleApplicationSettings;
using Domenici.Utilities.DependencyInjection.Plugins;
using Domenici.Utilities.DependencyInjection.Plugins.Generics;

namespace SampleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press a key to start...");
            Console.ReadKey();

            // Retrieve assembly signatures from configuration
            Console.WriteLine();
            Console.WriteLine("Retrieve assembly signatures from configuration...");
            string signatures = Settings.AssemblySignatures;

            #region Call services using base factory
            Console.WriteLine();
            Console.WriteLine("Call services using base factory...");

            // Use first service
            using (var factory = new PluginFactory(signatures))
            {
                IService service1 = (IService)factory.Create("Service1");
                Console.WriteLine($"Message from first service: { service1.GetMessage() }");
            }

            // Use second service
            using (var factory = new PluginFactory(signatures))
            {
                IService service2 = (IService)factory.Create("Service2");
                Console.WriteLine($"Message from second service: { service2.GetMessage() }");
            }

            // Use first service loaded from path
            using (var factory = new PluginFactory(signatures))
            {
                IService service1 = (IService)factory.Create("Service1FromPath");
                Console.WriteLine($"Message from first service loaded from path: { service1.GetMessage() }");
            }

            // Use second service loaded from path
            using (var factory = new PluginFactory(signatures))
            {
                IService service2 = (IService)factory.Create("Service2FromPath");
                Console.WriteLine($"Message from second service loaded from path: { service2.GetMessage() }");
            }
            #endregion

            #region Call services using the generics factory
            Console.WriteLine();
            Console.WriteLine("Call services using generics factory...");

            // Use first service
            using (var factory = new PluginFactory<IService>(signatures))
            {
                IService service1 = factory.Create("Service1");
                Console.WriteLine($"Message from first service: { service1.GetMessage() }");
            }

            // Use second service
            using (var factory = new PluginFactory<IService>(signatures))
            {
                IService service2 = factory.Create("Service2");
                Console.WriteLine($"Message from second service: { service2.GetMessage() }");
            }

            // Use first service loaded from path
            using (var factory = new PluginFactory<IService>(signatures))
            {
                IService service1 = factory.Create("Service1FromPath");
                Console.WriteLine($"Message from first service loaded from path: { service1.GetMessage() }");
            }

            // Use second service loaded from path
            using (var factory = new PluginFactory<IService>(signatures))
            {
                IService service2 = factory.Create("Service2FromPath");
                Console.WriteLine($"Message from second service loaded from path: { service2.GetMessage() }");
            }
            #endregion

            #region Call services using derived factory
            Console.WriteLine();
            Console.WriteLine("Call services using derived factory...");

            // Use first service
            using (var factory = new ServiceFactory<IService>(signatures))
            {
                IService service1 = factory.CreateService("Service1");
                Console.WriteLine($"Message from first service: { service1.GetMessage() }");
            }

            // Use second service
            using (var factory = new ServiceFactory<IService>(signatures))
            {
                IService service2 = factory.CreateService("Service2");
                Console.WriteLine($"Message from second service: { service2.GetMessage() }");
            }

            // Use first service loaded from path
            using (var factory = new ServiceFactory<IService>(signatures))
            {
                IService service1 = factory.CreateService("Service1FromPath");
                Console.WriteLine($"Message from first service loaded from path: { service1.GetMessage() }");
            }

            // Use second service loaded from path
            using (var factory = new ServiceFactory<IService>(signatures))
            {
                IService service2 = factory.CreateService("Service2FromPath");
                Console.WriteLine($"Message from second service loaded from path: { service2.GetMessage() }");
            }
            #endregion

            // Done
            Console.WriteLine();
            Console.WriteLine("Press a key to quit...");
            Console.ReadKey();
        }
    }
}
