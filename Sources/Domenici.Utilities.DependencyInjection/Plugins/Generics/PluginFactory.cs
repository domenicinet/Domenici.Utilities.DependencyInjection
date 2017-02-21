using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Domenici.Utilities.DependencyInjection.Plugins.Generics
{
    /// <summary>
    /// Objects that derive from this class will be able to instantiate
    /// objects from assemblies in the BIN folder.
    /// This class holds a list of AssemblySignatureItem (or it's JSON 
    /// representation) and will create an instance when being passed the
    /// key that identifies the assembly signature.
    /// 
    /// Singatures can be stored in JSON format, as follows: 
    ///     [
    ///         {
    ///             'key' : '1',
    ///             'signature' : 'AssemblyA, Version=1.0.0.0',
    ///             'namespace' : 'AssemplyA.SomeClass'
    ///         },
    ///         {
    ///             'key' : '2', 
    ///             'signature' : 'AssemblyB, Version=1.0.0.0, Culture=neutral',
    ///             'namespace' : 'AssemplyB.SomeClass'
    ///
    ///         },
    ///         {
    ///             'key'       : '3', 
    ///             'namespace' : 'AssemplyC.SomeClass'
    ///             'path'      : 'C:\\Libs\\AssemblyC.dll'
    ///         },
    ///         {
    ///             'key'       : '3', 
    ///             'namespace' : 'AssemplyD.SomeClass'
    ///             'path'      : '..\\..\\..\\Libs\\AssemblyD.dll'
    ///         }
    ///     ] 
    /// </summary>
    /// <example>
    /// Here is a sample class that inherits this class:
    ///     public class ServiceFactory : PluginFactory
    ///     {
    ///         public ServiceFactory(string jsonSignatures) : base(jsonSignatures) { }
    ///     
    ///         public IService CreateService(int id)
    ///         {
    ///             // Retrieve assembly key
    ///             string key = new SomeDataAccess.GetSignatureKey(id);
    ///     
    ///             // Return instance
    ///             return (IService)base.Create(key);
    ///         }
    ///     }
    /// 
    /// The sample class would then be used as follows:
    ///     using (var sf = new ServiceFactory(Settings.ServiceSignatures))
    ///     {
    ///         // Get instance
    ///         IService sm = sf.CreateService(someObject.ID);
    ///         
    ///         // Use instance
    ///         sm.DoSomething();
    ///     }
    /// 
    /// </example>
    public class PluginFactory<T> : IDisposable
    {   
        protected readonly bool mustDispose;

        /// <summary>
        /// List of items where key identifies the signature to be used 
        /// with Reflection in order to instantiate the correct class implementation.
        /// </summary>
        protected List<AssemblySignatureItem> reflectionSignatures;

        #region Constructors
        /// <summary>
        /// Initialize factory.
        /// </summary>
        /// <param name="assemblySignatures"></param>
        /// <param name="mustDispose"></param>
        public PluginFactory(List<AssemblySignatureItem> assemblySignatures, bool mustDispose = true)
        {
            this.mustDispose = mustDispose;

            reflectionSignatures = new List<AssemblySignatureItem>();
            reflectionSignatures.AddRange(assemblySignatures);
        }
        
        /// <summary>
        /// Initialize factory.
        /// </summary>
        /// <param name="mustDispose"></param>
        /// <param name="assemblySignaturesJson">
        /// Singatures are stored in JSON format, as follows: 
        ///     [
        ///         {
        ///             'key' : '1',
        ///             'signature' : 'AssemblyA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null',
        ///             'namespace' : 'AssemplyA.SomeClass'
        ///         },
        ///         {
        ///             'key' : '2', 
        ///             'signature' : 'AssemblyB, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null',
        ///             'namespace' : 'AssemplyB.SomeClass'
        ///
        ///         }
        ///     ]        
        /// </param>
        public PluginFactory(string assemblySignaturesJson = null, bool mustDispose = true)
        {
            this.mustDispose = mustDispose;

            if (null != assemblySignaturesJson)
            {
                // Retrieve all signatures from given JSON
                reflectionSignatures = JsonConvert.DeserializeObject<List<AssemblySignatureItem>>(assemblySignaturesJson);
            }
            else
            {
                // We don't need to retrieve the signature so we reuse the existing ones.
                return;
            }
        }
        #endregion

        #region Dispose
        public virtual void Dispose()
        {
            if (mustDispose)
            {
                // Nothing to do here.
            }
        }
        #endregion

        /// <summary>
        /// This method will instatiate an object from the assembly identified by the given key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Create(string key)
        {
            T result = default(T);

            try
            {
                if (null == reflectionSignatures)
                {
                    throw new PluginFactoryException("Reflection signatures have not been loaded.");
                }

                // Retrieve assembly name to be used by this merchant
                AssemblySignatureItem assemblyItem = reflectionSignatures.Find(x => x.Key == key);

                if (null != assemblyItem)
                {
                    if (string.IsNullOrWhiteSpace(assemblyItem.Path))
                    {
                        #region Retrieve assembly from BIN or GAC
                        Assembly assembly = Assembly.Load(assemblyItem.Signature);
                        result = (T)assembly.CreateInstance(assemblyItem.Namespace);
                        #endregion
                    }
                    else
                    {
                        #region Retrieve assembly from path in the signature file
                        Assembly assembly = Assembly.LoadFrom(Path.GetFullPath(assemblyItem.Path));
                        result = (T)assembly.CreateInstance(assemblyItem.Namespace);
                        #endregion
                    }
                }
                else
                {
                    throw new PluginFactoryException($"Cannot find reflection signature with key: { key }");
                }
            }
            catch (Exception e)
            {
                throw new PluginFactoryException($"Unexpected error while instantiating object for key: '{ key }' (details follow): { e.ToString() }");
            }

            return result;
        }        
    }
}
