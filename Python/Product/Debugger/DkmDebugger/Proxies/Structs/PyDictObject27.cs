using System.Collections.Generic;
using System.Linq;
using Microsoft.PythonTools.Infrastructure;
using Microsoft.PythonTools.Parsing;
using Microsoft.VisualStudio.Debugger;

namespace Microsoft.PythonTools.DkmDebugger.Proxies.Structs {
    [StructProxy(StructName = "PyDictObject", MaxVersion = PythonLanguageVersion.V27)]
    [PyType(VariableName = "PyDict_Type", MaxVersion = PythonLanguageVersion.V27)]
    internal class PyDictObject27 : PyDictObject {
        private class DummyHolder : DkmDataItem {
            public readonly PointerProxy<PyObject> Dummy;

            public DummyHolder(DkmProcess process) {
                Dummy = process.GetPythonRuntimeInfo().DLLs.Python.GetStaticVariable<PointerProxy<PyObject>>("dummy", "dictobject.obj");
            }
        }

        private class Fields {
            public StructField<SSizeTProxy> ma_mask;
            public StructField<PointerProxy<ArrayProxy<PyDictKeyEntry>>> ma_table;
        }

        private readonly Fields _fields;
        private readonly PyObject _dummy;

        public PyDictObject27(DkmProcess process, ulong address)
            : base(process, address) {
            InitializeStruct(this, out _fields);
            CheckPyType<PyDictObject27>();

            _dummy = Process.GetOrCreateDataItem(() => new DummyHolder(Process)).Dummy.TryRead();
        }

        public PointerProxy<ArrayProxy<PyDictKeyEntry>> ma_table {
            get { return GetFieldProxy(_fields.ma_table); }
        }

        public SSizeTProxy ma_mask {
            get { return GetFieldProxy(_fields.ma_mask); }
        }

        public override IEnumerable<KeyValuePair<PyObject, PointerProxy<PyObject>>> ReadElements() {
            if (ma_table.IsNull) {
                return Enumerable.Empty<KeyValuePair<PyObject, PointerProxy<PyObject>>>();
            }

            var count = ma_mask.Read() + 1;
            var entries = ma_table.Read().Take(count);
            var items = from entry in entries
                        let key = entry.me_key.TryRead()
                        where key != null && key != _dummy
                        let valuePtr = entry.me_value
                        where !valuePtr.IsNull
                        select new KeyValuePair<PyObject, PointerProxy<PyObject>>(key, valuePtr);
            return items;
        }
    }
}
