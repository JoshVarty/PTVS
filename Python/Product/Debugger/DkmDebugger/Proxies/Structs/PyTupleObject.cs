using System.Collections.Generic;
using System.Linq;
using Microsoft.PythonTools.Infrastructure;
using Microsoft.VisualStudio.Debugger;
using Microsoft.VisualStudio.Debugger.Evaluation;

namespace Microsoft.PythonTools.DkmDebugger.Proxies.Structs {
    internal class PyTupleObject : PyVarObject {
        private class Fields {
            public StructField<ArrayProxy<PointerProxy<PyObject>>> ob_item;
        }

        private readonly Fields _fields;

        public PyTupleObject(DkmProcess process, ulong address)
            : base(process, address) {
            InitializeStruct(this, out _fields);
            CheckPyType<PyTupleObject>();
        }

        public ArrayProxy<PointerProxy<PyObject>> ob_item {
            get { return GetFieldProxy(_fields.ob_item); }
        }

        public IEnumerable<PointerProxy<PyObject>> ReadElements() {
            return ob_item.Take(ob_size.Read());
        }

        public override void Repr(ReprBuilder builder) {
            var count = ob_size.Read();
            if (count > ReprBuilder.MaxJoinedItems) {
                builder.AppendFormat("<tuple, len() = {0}>", count);
            } else {
                builder.Append("(");
                builder.AppendJoined(", ", ReadElements(), obj => builder.AppendRepr(obj.TryRead()));
                if (ob_size.Read() == 1) {
                    builder.Append(",");
                }
                builder.Append(")");
            }
        }

        public override IEnumerable<PythonEvaluationResult> GetDebugChildren(ReprOptions reprOptions) {
            yield return new PythonEvaluationResult(new ValueStore<long>(ob_size.Read()), "len()") {
                Category = DkmEvaluationResultCategory.Method
            };

            foreach (var item in ReadElements()) {
                yield return new PythonEvaluationResult(item);
            }
        }
    }
}
