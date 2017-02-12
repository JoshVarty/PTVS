using Microsoft.PythonTools.Interpreter;

namespace Microsoft.IronPythonTools.Interpreter {
    class IronPythonGenericMember : PythonObject {
        private readonly PythonMemberType _type;

        public IronPythonGenericMember(IronPythonInterpreter interpreter, ObjectIdentityHandle obj, PythonMemberType type)
            : base(interpreter, obj) {
            _type = type;
        }

        public override PythonMemberType MemberType {
            get {
                return _type;
            }
        }
    }
}
