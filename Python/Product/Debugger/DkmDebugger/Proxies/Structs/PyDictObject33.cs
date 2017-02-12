using System.Collections.Generic;
using System.Linq;
using Microsoft.PythonTools.Infrastructure;
using Microsoft.PythonTools.Parsing;
using Microsoft.VisualStudio.Debugger;

namespace Microsoft.PythonTools.DkmDebugger.Proxies.Structs {
    [StructProxy(StructName = "PyDictObject", MinVersion = PythonLanguageVersion.V33, MaxVersion = PythonLanguageVersion.V35)]
    [PyType(VariableName = "PyDict_Type", MinVersion = PythonLanguageVersion.V33, MaxVersion = PythonLanguageVersion.V35)]
    internal class PyDictObject33 : PyDictObject {
        private class Fields {
            public StructField<PointerProxy<PyDictKeysObject33>> ma_keys;
            public StructField<PointerProxy<ArrayProxy<PointerProxy<PyObject>>>> ma_values;
        }

        private readonly Fields _fields;

        public PyDictObject33(DkmProcess process, ulong address)
            : base(process, address) {
            InitializeStruct(this, out _fields);
            CheckPyType<PyDictObject33>();
        }

        public PointerProxy<PyDictKeysObject33> ma_keys {
            get { return GetFieldProxy(_fields.ma_keys); }
        }

        public PointerProxy<ArrayProxy<PointerProxy<PyObject>>> ma_values {
            get { return GetFieldProxy(_fields.ma_values); }
        }

        public override IEnumerable<KeyValuePair<PyObject, PointerProxy<PyObject>>> ReadElements() {
            if (ma_keys.IsNull) {
                yield break;
            }

            var keys = this.ma_keys.Read();
            var entries = keys.dk_entries.Take(keys.dk_size.Read());

            var ma_values = this.ma_values;
            if (ma_values.IsNull) {
                foreach (var entry in entries) {
                    var key = entry.me_key.TryRead();
                    if (key != null) {
                        yield return new KeyValuePair<PyObject, PointerProxy<PyObject>>(key, entry.me_value);
                    }
                }
            } else {
                var valuePtr = ma_values.Read()[0];
                foreach (var entry in entries) {
                    var key = entry.me_key.TryRead();
                    if (key != null) {
                        yield return new KeyValuePair<PyObject, PointerProxy<PyObject>>(key, valuePtr);
                    }
                    valuePtr = valuePtr.GetAdjacentProxy(1);
                }
            }
        }
    }

    [StructProxy(MinVersion = PythonLanguageVersion.V33, MaxVersion = PythonLanguageVersion.V35, StructName = "_dictkeysobject")]
    internal class PyDictKeysObject33 : StructProxy {
        private class Fields {
            public StructField<SSizeTProxy> dk_size;
            public StructField<ArrayProxy<PyDictKeyEntry>> dk_entries;
        }

        private readonly Fields _fields;

        public SSizeTProxy dk_size {
            get { return GetFieldProxy(_fields.dk_size); }
        }

        public ArrayProxy<PyDictKeyEntry> dk_entries {
            get { return GetFieldProxy(_fields.dk_entries); }
        }

        public PyDictKeysObject33(DkmProcess process, ulong address)
            : base(process, address) {
            InitializeStruct(this, out _fields);
        }
    }
}
