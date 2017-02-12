using Microsoft.VisualStudio.Debugger;

namespace Microsoft.PythonTools.DkmDebugger.Proxies.Structs {
    [StructProxy(StructName = "PyObject")]
    internal class PyEllipsisObject : PyObject {
        public PyEllipsisObject(DkmProcess process, ulong address)
            : base(process, address) {
            CheckPyType<PyEllipsisObject>();
        }

        public override void Repr(ReprBuilder builder) {
            builder.Append("...");
        }
    }
}
