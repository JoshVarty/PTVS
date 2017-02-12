using Microsoft.VisualStudio.Debugger;

namespace Microsoft.PythonTools.DkmDebugger.Proxies.Structs {
    internal class PyMethodDef : StructProxy {
        private class Fields {
            public StructField<PointerProxy> ml_meth;
        }

        private readonly Fields _fields;

        public PyMethodDef(DkmProcess process, ulong address)
            : base(process, address) {
            InitializeStruct(this, out _fields);
        }

        public PointerProxy ml_meth {
            get { return GetFieldProxy(_fields.ml_meth); }
        }
    }
}
