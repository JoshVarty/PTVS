using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Text.Adornments;
using Microsoft.VisualStudio.Utilities;

namespace TestUtilities.Mocks {
    public class MockComponentModel : ExportProvider, IComponentModel {
        public readonly Dictionary<Type, List<Lazy<object>>> Extensions = new Dictionary<Type, List<Lazy<object>>>();

        public void AddExtension<T>(Func<T> creator) where T : class {
            AddExtension(typeof(T), creator);
        }

        public void AddExtension<T>(Type key, Func<T> creator) where T : class {
            List<Lazy<object>> extensions;
            if (!Extensions.TryGetValue(key, out extensions)) {
                Extensions[key] = extensions = new List<Lazy<object>>();
            }
            extensions.Add(new Lazy<object>(creator));
        }

        public T GetService<T>() where T : class {
            List<Lazy<object>> extensions;
            if (Extensions.TryGetValue(typeof(T), out extensions)) {
                Debug.Assert(extensions.Count == 1, "Multiple extensions were registered");
                return (T)extensions[0].Value;
            }
            Console.WriteLine("Unregistered component model service " + typeof(T).FullName);
            return null;
        }

        public System.ComponentModel.Composition.Primitives.ComposablePartCatalog DefaultCatalog {
            get { throw new NotImplementedException(); }
        }

        public System.ComponentModel.Composition.ICompositionService DefaultCompositionService {
            get { throw new NotImplementedException(); }
        }

        public System.ComponentModel.Composition.Hosting.ExportProvider DefaultExportProvider {
            get { return this; }
        }

        public System.ComponentModel.Composition.Primitives.ComposablePartCatalog GetCatalog(string catalogName) {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetExtensions<T>() where T : class {
            List<Lazy<object>> res;
            if (Extensions.TryGetValue(typeof(T), out res)) {
                foreach (var t in res) {
                    yield return (T)t.Value;
                }
            }
        }

        protected override IEnumerable<Export> GetExportsCore(ImportDefinition definition, AtomicComposition atomicComposition) {
            foreach (var keyValue in Extensions) {
                if (keyValue.Key.FullName == definition.ContractName) {
                    foreach (var value in keyValue.Value) {
                        yield return new Export(
                            new ExportDefinition(keyValue.Key.FullName, new Dictionary<string, object>()),
                            () => value.Value
                        );
                    }
                }
            }
        }
    }
}
