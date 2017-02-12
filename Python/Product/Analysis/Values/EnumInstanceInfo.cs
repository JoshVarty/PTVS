using Microsoft.PythonTools.Interpreter;

namespace Microsoft.PythonTools.Analysis.Values {
    internal class EnumInstanceInfo : ConstantInfo {
        public EnumInstanceInfo(object value, PythonAnalyzer projectState)
            : base(projectState.ClassInfos[BuiltinTypeId.Int], value, PythonMemberType.EnumInstance) {
        }

        public override string Description {
            get {
                return Value.ToString();
            }
        }
    }
}
