using IronPython.Runtime.Operations;
using IronPython.Runtime.Types;
using Microsoft.PythonTools.Interpreter;

namespace Microsoft.IronPythonTools.Interpreter {
    class IronPythonBuiltinMethodDescriptor : PythonObject, IPythonMethodDescriptor {
        private IPythonFunction _function;

        public IronPythonBuiltinMethodDescriptor(IronPythonInterpreter interpreter, ObjectIdentityHandle desc)
            : base(interpreter, desc) {
        }

        #region IBuiltinMethodDescriptor Members

        public IPythonFunction Function {
            get {
                if (_function == null) {
                    var ri = RemoteInterpreter;
                    if (ri != null) {
                        var func = ri.GetBuiltinMethodDescriptorTemplate(Value);

                        _function = (IPythonFunction)Interpreter.MakeObject(func);
                    }
                }
                return _function;
            }
        }

        public bool IsBound {
            get {
                return false;
            }
        }

        #endregion

        #region IMember Members

        public override PythonMemberType MemberType {
            get { return PythonMemberType.Method; }
        }

        #endregion
    }
}
