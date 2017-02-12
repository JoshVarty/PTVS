using System.Collections.Generic;
using Microsoft.PythonTools.Parsing;
using Microsoft.VisualStudio.Debugger;

namespace Microsoft.PythonTools.DkmDebugger.Proxies.Structs {
    internal class PyInterpreterState : StructProxy {
        private class Fields {
            public StructField<PointerProxy<PyInterpreterState>> next;
            public StructField<PointerProxy<PyThreadState>> tstate_head;
            public StructField<PointerProxy<PyDictObject>> modules;
            [FieldProxy(MinVersion = PythonLanguageVersion.V36)]
            public StructField<PointerProxy> eval_frame;
        }

        private readonly Fields _fields;

        public PyInterpreterState(DkmProcess process, ulong address)
            : base(process, address) {
            InitializeStruct(this, out _fields);
        }

        public static PyInterpreterState TryCreate(DkmProcess process, ulong address) {
            if (address == 0) {
                return null;
            }

            return new PyInterpreterState(process, address);
        }

        public PointerProxy<PyInterpreterState> next {
            get { return GetFieldProxy(_fields.next); }
        }

        public PointerProxy<PyThreadState> tstate_head {
            get { return GetFieldProxy(_fields.tstate_head); }
        }

        public PointerProxy<PyDictObject> modules {
            get { return GetFieldProxy(_fields.modules); }
        }

        public PointerProxy eval_frame {
            get { return GetFieldProxy(_fields.eval_frame); }
        }

        private class InterpHeadHolder : DkmDataItem {
            public readonly PointerProxy<PyInterpreterState> Proxy;

            public InterpHeadHolder(DkmProcess process) {
                Proxy = process.GetPythonRuntimeInfo().DLLs.Python.GetStaticVariable<PointerProxy<PyInterpreterState>>("interp_head");
            }
        }

        public static PointerProxy<PyInterpreterState> interp_head(DkmProcess process) {
            return process.GetOrCreateDataItem(() => new InterpHeadHolder(process)).Proxy;
        }

        public static IEnumerable<PyInterpreterState> GetInterpreterStates(DkmProcess process) {
            for (var interp = interp_head(process).TryRead(); interp != null; interp = interp.next.TryRead()) {
                yield return interp;
            }
        }

        public IEnumerable<PyThreadState> GetThreadStates() {
            for (var tstate = tstate_head.TryRead(); tstate != null; tstate = tstate.next.TryRead()) {
                yield return tstate;
            }
        }
    }
}
