using IronPython.Runtime.Types;
using Microsoft.PythonTools.Interpreter;

namespace Microsoft.IronPythonTools.Interpreter {
    class IronPythonField : PythonObject, IBuiltinProperty {
        private IPythonType _fieldType;
        private bool? _isStatic;

        public IronPythonField(IronPythonInterpreter interpreter, ObjectIdentityHandle field)
            : base(interpreter, field) {
        }

        #region IBuiltinProperty Members

        public IPythonType Type {
            get {
                if (_fieldType == null) {
                    var ri = RemoteInterpreter;
                    _fieldType = ri != null ? (IPythonType)Interpreter.MakeObject(ri.GetFieldType(Value)) : null;
                }
                return _fieldType;
            }
        }

        public bool IsStatic {
            get {
                if (_isStatic == null) {
                    var ri = RemoteInterpreter;
                    _isStatic = ri != null ? ri.IsFieldStatic(Value) : false;
                }

                return _isStatic.Value;
            }
        }

        public string Documentation {
            get {
                var ri = RemoteInterpreter;
                return ri != null ? ri.GetFieldDocumentation(Value) : string.Empty;
            }
        }

        public string Description {
            get { return Documentation; }
        }

        public override PythonMemberType MemberType {
            get { return PythonMemberType.Field; }
        }

        #endregion
    }
}
