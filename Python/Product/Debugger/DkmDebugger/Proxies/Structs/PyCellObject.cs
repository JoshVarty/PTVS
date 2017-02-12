using Microsoft.VisualStudio.Debugger;

namespace Microsoft.PythonTools.DkmDebugger.Proxies.Structs {
    internal class PyCellObject : PyObject {
        public class Fields {
            public StructField<PointerProxy<PyObject>> ob_ref;
        }

        private readonly Fields _fields;

        public PyCellObject(DkmProcess process, ulong address)
            : base(process, address) {
            InitializeStruct(this, out _fields);
            CheckPyType<PyCellObject>();
        }

        public PointerProxy<PyObject> ob_ref {
            get { return GetFieldProxy(_fields.ob_ref); }
        }

        public override void Repr(ReprBuilder builder) {
            builder.AppendFormat("<cell at {0:PTR}: ", Address);

            var obj = ob_ref.TryRead();
            if (obj != null) {
                builder.AppendFormat("{0} object at {1:PTR}>", obj.ob_type.Read().tp_name.Read().ToString(), obj.Address);
            } else {
                builder.Append("empty>");
            }
        }
    }
}
