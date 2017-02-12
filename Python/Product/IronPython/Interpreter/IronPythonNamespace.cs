using System.Collections.Generic;
using System.Linq;
using Microsoft.PythonTools.Interpreter;
using Microsoft.Scripting.Actions;

namespace Microsoft.IronPythonTools.Interpreter {
    class IronPythonNamespace : PythonObject, IPythonModule {
        public IronPythonNamespace(IronPythonInterpreter interpreter, ObjectIdentityHandle ns)
            : base(interpreter, ns) {
        }

        #region IPythonModule Members

        public string Name {
            get {
                var ri = RemoteInterpreter;
                return ri != null ? ri.GetNamespaceName(Value) : string.Empty;
            }
        }

        public void Imported(IModuleContext context) {
            ((IronPythonModuleContext)context).ShowClr = true;
        }

        public IEnumerable<string> GetChildrenModules() {
            var ri = RemoteInterpreter;
            return ri != null ? ri.GetNamespaceChildren(Value) : Enumerable.Empty<string>();
        }

        public string Documentation {
            get { return string.Empty; }
        }

        #endregion
    }
}
