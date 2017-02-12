using System;
using IronPython.Runtime.Types;
using Microsoft.PythonTools.Interpreter;

namespace Microsoft.IronPythonTools.Interpreter {
    class IronPythonProperty : PythonObject, IBuiltinProperty {
        private IPythonType _propertyType;
        private bool? _isStatic;

        public IronPythonProperty(IronPythonInterpreter interpreter, ObjectIdentityHandle property)
            : base(interpreter, property) {
        }

        #region IBuiltinProperty Members

        public IPythonType Type {
            get {
                if (_propertyType == null) {
                    var ri = RemoteInterpreter;
                    _propertyType = ri != null ? (IPythonType)Interpreter.MakeObject(ri.GetPropertyType(Value)) : null;
                }
                return _propertyType;
            }
        }

        public bool IsStatic {
            get {
                if (_isStatic == null) {
                    var ri = RemoteInterpreter;
                    _isStatic = ri != null ? ri.IsPropertyStatic(Value) : false;
                }
                return _isStatic.Value;
            }
        }

        public string Documentation {
            get {
                var ri = RemoteInterpreter;
                return ri != null ? ri.GetPropertyDocumentation(Value) : string.Empty;
            }
        }

        public string Description {
            get {
                if (Type == null) {
                    return "property of unknown type";
                } else {
                    return "property of type " + Type.Name;
                }
            }
        }

        public override PythonMemberType MemberType {
            get { return PythonMemberType.Property; }
        }

        #endregion
    }
}
