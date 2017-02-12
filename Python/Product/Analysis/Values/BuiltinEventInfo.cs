using Microsoft.PythonTools.Analysis.Analyzer;
using Microsoft.PythonTools.Interpreter;
using Microsoft.PythonTools.Parsing.Ast;

namespace Microsoft.PythonTools.Analysis.Values {
    internal class BuiltinEventInfo : BuiltinNamespace<IPythonType> {
        private readonly IPythonEvent _value;
        private string _doc;

        public BuiltinEventInfo(IPythonEvent value, PythonAnalyzer projectState)
            : base(value.EventHandlerType, projectState) {
            _value = value;
            _doc = null;
        }

        public override void AugmentAssign(AugmentedAssignStatement node, AnalysisUnit unit, IAnalysisSet value) {
            base.AugmentAssign(node, unit, value);
            var args = GetEventInvokeArgs(ProjectState);
            foreach (var r in value) {
                r.Call(node, unit, args, ExpressionEvaluator.EmptyNames);
            }
        }

        internal IAnalysisSet[] GetEventInvokeArgs(PythonAnalyzer state) {
            var p = _value.GetEventParameterTypes();

            var args = new IAnalysisSet[p.Count];
            for (int i = 0; i < p.Count; i++) {
                args[i] = state.GetInstance(p[i]).SelfSet;
            }
            return args;
        }

        public override string Description {
            get {
                return "event of type " + _value.EventHandlerType.Name;
            }
        }

        public override PythonMemberType MemberType {
            get {
                return _value.MemberType;
            }
        }

        public override string Documentation {
            get {
                if (_doc == null) {
                    _doc = Utils.StripDocumentation(_value.Documentation);
                }
                return _doc;
            }
        }

        public override ILocatedMember GetLocatedMember() {
            return _value as ILocatedMember;
        }
    }
}
