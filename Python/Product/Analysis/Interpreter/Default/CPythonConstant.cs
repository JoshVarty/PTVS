
namespace Microsoft.PythonTools.Interpreter.Default {
    class CPythonConstant : IPythonConstant {
        private readonly IPythonType _type;

        public CPythonConstant(IPythonType type) {
            _type = type;
        }

        #region IPythonConstant Members

        public IPythonType Type {
            get { return _type; }
        }

        #endregion

        #region IMember Members

        public PythonMemberType MemberType {
            get { return PythonMemberType.Constant; }
        }

        #endregion
    }
}
