using Microsoft.VisualStudio.Debugger;

namespace Microsoft.PythonTools.DkmDebugger.Proxies.Structs {
    [PyType(VariableName = "_PyExc_BaseException")]
    internal class PyBaseExceptionObject : PyObject {
        public class Fields {
            public StructField<PointerProxy<PyObject>> args;
        }

        private readonly Fields _fields;

        public PyBaseExceptionObject(DkmProcess process, ulong address)
            : base(process, address) {
            InitializeStruct(this, out _fields);
            CheckPyType<PyBaseExceptionObject>();
        }

        public PointerProxy<PyObject> args {
            get { return GetFieldProxy(_fields.args); }
        }
    }
}
