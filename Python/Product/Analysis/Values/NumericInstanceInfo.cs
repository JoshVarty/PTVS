using Microsoft.PythonTools.Interpreter;
using Microsoft.PythonTools.Parsing;
using Microsoft.PythonTools.Parsing.Ast;

namespace Microsoft.PythonTools.Analysis.Values {
    class NumericInstanceInfo : BuiltinInstanceInfo {
        public NumericInstanceInfo(BuiltinClassInfo klass)
            : base(klass) {
        }

        public override IAnalysisSet BinaryOperation(Node node, AnalysisUnit unit, PythonOperator operation, IAnalysisSet rhs) {
            switch (operation) {
                case PythonOperator.GreaterThan:
                case PythonOperator.LessThan:
                case PythonOperator.LessThanOrEqual:
                case PythonOperator.GreaterThanOrEqual:
                case PythonOperator.Equal:
                case PythonOperator.NotEqual:
                case PythonOperator.Is:
                case PythonOperator.IsNot:
                    return ProjectState.ClassInfos[BuiltinTypeId.Bool].Instance;
                case PythonOperator.TrueDivide:
                case PythonOperator.Add:
                case PythonOperator.Subtract:
                case PythonOperator.Multiply:
                case PythonOperator.MatMultiply:
                case PythonOperator.Divide:
                case PythonOperator.Mod:
                case PythonOperator.BitwiseAnd:
                case PythonOperator.BitwiseOr:
                case PythonOperator.Xor:
                case PythonOperator.LeftShift:
                case PythonOperator.RightShift:
                case PythonOperator.Power:
                case PythonOperator.FloorDivide:
                    return ConstantInfo.NumericOp(node, this, unit, operation, rhs) ?? CallReverseBinaryOp(node, unit, operation, rhs);
            }
            return CallReverseBinaryOp(node, unit, operation, rhs);
        }
    }
}
