using Microsoft.VisualStudio.Debugger;

namespace Microsoft.PythonTools.DkmDebugger.Proxies.Structs {
    internal class PyCFunctionObject : PyObject {
        private class Fields {
            public StructField<PointerProxy<PyMethodDef>> m_ml;
        }

        private readonly Fields _fields;

        public PyCFunctionObject(DkmProcess process, ulong address)
            : base(process, address) {
            InitializeStruct(this, out _fields);
            CheckPyType<PyCFunctionObject>();
        }

        public PointerProxy<PyMethodDef> m_ml {
            get { return GetFieldProxy(_fields.m_ml); }
        }
    }
}
