using System;
using System.Diagnostics;
using Microsoft.VisualStudio.Debugger;

namespace Microsoft.PythonTools.DkmDebugger.Proxies.Structs {
    internal class PyFloatObject : PyObject {
        private class Fields {
            public StructField<DoubleProxy> ob_fval;
        }

        private readonly Fields _fields;

        public PyFloatObject(DkmProcess process, ulong address)
            : base(process, address) {
            InitializeStruct(this, out _fields);
            CheckPyType<PyFloatObject>();
        }

        public static PyFloatObject Create(DkmProcess process, double value) {
            var allocator = process.GetDataItem<PyObjectAllocator>();
            Debug.Assert(allocator != null);

            var result = allocator.Allocate<PyFloatObject>();
            result.ob_fval.Write(value);
            return result;
        }

        private DoubleProxy ob_fval {
            get { return GetFieldProxy(_fields.ob_fval); }
        }

        public Double ToDouble() {
            return ob_fval.Read();
        }

        public override void Repr(ReprBuilder builder) {
            builder.AppendLiteral(ToDouble());
        }
    }
}
