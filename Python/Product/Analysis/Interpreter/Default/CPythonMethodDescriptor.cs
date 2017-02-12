using System.Collections.Generic;

namespace Microsoft.PythonTools.Interpreter.Default {
    class CPythonMethodDescriptor : IPythonMethodDescriptor {
        private readonly string _name;
        private readonly CPythonFunction _func;
        private readonly bool _isBound;

        public CPythonMethodDescriptor(ITypeDatabaseReader typeDb, string name, Dictionary<string, object> valueDict, IMemberContainer declaringType) {
            _name = name;
            _func = new CPythonFunction(typeDb, name, valueDict, declaringType, isMethod: true);
            object value;
            if (valueDict.TryGetValue("bound", out value)) {
                _isBound = (value as bool?) ?? false;
            }
        }

        #region IBuiltinMethodDescriptor Members

        public IPythonFunction Function {
            get { return _func;  }
        }

        public bool IsBound {
            get { return _isBound; }
        }

        #endregion

        #region IMember Members

        public PythonMemberType MemberType {
            get { return PythonMemberType.Method; }
        }

        #endregion
    }
}
