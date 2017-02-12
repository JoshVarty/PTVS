using System;
using IronPython.Runtime.Types;
using Microsoft.PythonTools.Interpreter;

namespace Microsoft.IronPythonTools.Interpreter {
    class IronPythonExtensionProperty : PythonObject, IBuiltinProperty {
        private IPythonType _propertyType;

        public IronPythonExtensionProperty(IronPythonInterpreter interpreter, ObjectIdentityHandle property)
            : base(interpreter, property) {
        }

        #region IBuiltinProperty Members

        public IPythonType Type {
            get {
                if (_propertyType == null) {
                    var ri = RemoteInterpreter;
                    _propertyType = ri != null ? Interpreter.GetTypeFromType(ri.GetExtensionPropertyType(Value)) : null;
                }
                return _propertyType;
            }
        }

        public bool IsStatic {
            get {
                return false;
            }
        }

        public string Documentation {
            get {
                var ri = RemoteInterpreter;
                return ri != null ? ri.GetExtensionPropertyDocumentation(Value) : string.Empty;
            }
        }

        public string Description {
            get { return Documentation; }
        }

        public override PythonMemberType MemberType {
            get { return PythonMemberType.Property; }
        }

        #endregion
    }
}
