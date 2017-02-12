using Microsoft.VisualStudio.Debugger;

namespace Microsoft.PythonTools.DkmDebugger.Proxies.Structs {
    internal class PyModuleObject : PyObject {
        private class Fields {
            public StructField<PointerProxy<PyDictObject>> md_dict;
        }

        private readonly Fields _fields;

        public PyModuleObject(DkmProcess process, ulong address)
            : base(process, address) {
            InitializeStruct(this, out _fields);
        }

        public PointerProxy<PyDictObject> md_dict {
            get { return GetFieldProxy(_fields.md_dict); }
        }
    }
}
