using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;

namespace Microsoft.PythonTools.Interpreter {
    class MockExportProvider : ExportProvider {
        readonly Dictionary<ExportDefinition, Export> _exports;

        private static ExportDefinition MakeDefinition(Type type) {
            return new ExportDefinition(
                type.FullName,
                new Dictionary<string, object> {
                        { "ExportTypeIdentity", type.FullName }
                    }
            );
        }

        public MockExportProvider() {
            _exports = new Dictionary<ExportDefinition, Export>();
        }

        public void SetExport(Type identity, Func<object> getter) {
            var definition = MakeDefinition(identity);
            _exports[definition] = new Export(definition, getter);
        }

        protected override IEnumerable<Export> GetExportsCore(
            ImportDefinition definition,
            AtomicComposition atomicComposition
        ) {

            return from kv in _exports
                   where definition.IsConstraintSatisfiedBy(kv.Key)
                   select kv.Value;
        }
    }
}
