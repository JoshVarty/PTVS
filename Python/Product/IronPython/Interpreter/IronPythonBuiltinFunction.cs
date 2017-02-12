using System;
using System.Collections.Generic;
using IronPython.Runtime.Types;
using Microsoft.PythonTools.Interpreter;

namespace Microsoft.IronPythonTools.Interpreter {
    class IronPythonBuiltinFunction : PythonObject, IPythonFunction {
        private IronPythonBuiltinFunctionTarget[] _targets;
        private IPythonType _declaringType;
        private IPythonModule _declaringModule;

        public IronPythonBuiltinFunction(IronPythonInterpreter interpreter, ObjectIdentityHandle function)
            : base(interpreter, function) {
        }

        #region IBuiltinFunction Members

        public string Name {
            get {
                var ri = RemoteInterpreter;
                return ri != null ? ri.GetBuiltinFunctionName(Value) : string.Empty;
            }
        }

        public string Documentation {
            get {
                var ri = RemoteInterpreter;
                return ri != null ? ri.GetBuiltinFunctionDocumentation(Value) : string.Empty;
            }
        }

        public IList<IPythonFunctionOverload> Overloads {
            get {
                // skip methods that are virtual base helpers (e.g. methods like
                // object.Equals(Object_#1, object other))

                if (_targets == null) {
                    var ri = RemoteInterpreter;
                    var overloads = ri != null ? ri.GetBuiltinFunctionOverloads(Value) : new ObjectIdentityHandle[0];
                    var result = new IronPythonBuiltinFunctionTarget[overloads.Length];
                    var decltype = (IronPythonType)DeclaringType;
                    for(int i = 0; i<overloads.Length; i++){
                        result[i] = new IronPythonBuiltinFunctionTarget(
                            Interpreter, 
                            overloads[i], 
                            decltype
                        );

                    }

                    _targets = result;
                }

                return _targets;
            }
        }

        public IPythonType DeclaringType {
            get {
                if (_declaringType == null) {
                    var ri = RemoteInterpreter;
                    _declaringType = ri != null ? Interpreter.GetTypeFromType(ri.GetBuiltinFunctionDeclaringPythonType(Value)) : null;
                }
                return _declaringType;
            }
        }

        public bool IsBuiltin {
            get {
                return true;
            }
        }

        public bool IsStatic {
            get {
                return true;
            }
        }

        public bool IsClassMethod {
            get {
                return false;
            }
        }

        public IPythonModule DeclaringModule {
            get {
                if (_declaringModule == null) {
                    var ri = RemoteInterpreter;
                    _declaringModule = ri != null ? Interpreter.GetModule(ri.GetBuiltinFunctionModule(Value)) : null;
                }
                return _declaringModule;
            }
        }

        #endregion

        #region IMember Members

        public override PythonMemberType MemberType {
            get { return PythonMemberType.Function; }
        }

        #endregion

    }
}
