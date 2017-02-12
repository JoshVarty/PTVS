using System.Collections.Generic;
using System.Linq;
using Microsoft.PythonTools.Interpreter;
using Microsoft.PythonTools.Parsing.Ast;

namespace Microsoft.PythonTools.Analysis.Values {
    class ObjectBuiltinClassInfo : BuiltinClassInfo {
        private AnalysisValue _new;
        private AnalysisValue _setattr;

        public ObjectBuiltinClassInfo(IPythonType classObj, PythonAnalyzer projectState)
            : base(classObj, projectState) {
        }

        public override IAnalysisSet GetMember(Node node, AnalysisUnit unit, string name) {
            var res = base.GetMember(node, unit, name);

            switch (name) {
                case "__new__":
                    return _new = _new ?? new SpecializedCallable(
                        res.OfType<BuiltinNamespace<IPythonType>>().FirstOrDefault(),
                        ObjectNew,
                        false
                    );
                case "__setattr__":
                    return _setattr = _setattr ?? new SpecializedCallable(
                        res.OfType<BuiltinNamespace<IPythonType>>().FirstOrDefault(),
                        ObjectSetAttr,
                        false
                    );
            }

            return res;
        }

        private IAnalysisSet ObjectNew(Node node, Analysis.AnalysisUnit unit, IAnalysisSet[] args, NameExpression[] keywordArgNames) {
            if (args.Length >= 1) {
                var instance = AnalysisSet.Empty;
                foreach (var n in args[0]) {
                    var bci = n as BuiltinClassInfo;
                    var ci = n as ClassInfo;
                    if (bci != null) {
                        instance = instance.Union(bci.Instance);
                    } else if (ci != null) {
                        instance = instance.Union(ci.Instance);
                    }
                }
                return instance;
            }
            return ProjectState.ClassInfos[BuiltinTypeId.Object].Instance;
        }

        private IAnalysisSet ObjectSetAttr(Node node, Analysis.AnalysisUnit unit, IAnalysisSet[] args, NameExpression[] keywordArgNames) {
            if (args.Length >= 3) {
                foreach (var ii in args[0].OfType<InstanceInfo>()) {
                    foreach (var key in args[1].GetConstantValueAsString()) {
                        ii.SetMember(node, unit, key, args[2]);
                    }
                }
            }
            return AnalysisSet.Empty;
        }

    }
}
