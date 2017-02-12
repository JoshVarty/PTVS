using Microsoft.PythonTools.Analysis.Analyzer;
using Microsoft.PythonTools.Interpreter;
using Microsoft.PythonTools.Parsing.Ast;

namespace Microsoft.PythonTools.Analysis.Values {
    class DictBuiltinClassInfo : BuiltinClassInfo {
        public DictBuiltinClassInfo(IPythonType classObj, PythonAnalyzer projectState)
            : base(classObj, projectState) {
        }

        public override IAnalysisSet Call(Node node, AnalysisUnit unit, IAnalysisSet[] args, NameExpression[] keywordArgNames) {
            var res = (DictionaryInfo)unit.Scope.GetOrMakeNodeValue(
                node,
                NodeValueKind.Dictionary,
                (node_) => new DictionaryInfo(unit.ProjectEntry, node)
            );

            if (keywordArgNames.Length > 0) {
                for (int i = 0; i < keywordArgNames.Length; i++) {
                    var curName = keywordArgNames[i].Name;
                    var curArg = args[args.Length - keywordArgNames.Length + i];
                    if (curName == "**") {
                        foreach (var value in curArg) {
                            CopyFrom(args, res);
                        }
                    } else if (curName != "*") {
                        res.AddTypes(
                            node,
                            unit,
                            ProjectState.GetConstant(curName),
                            curArg
                        );
                    }
                }
            } else if (args.Length == 1) {
                foreach (var value in args[0]) {
                    CopyFrom(args, res);
                }
            }
            return res;
        }

        private static void CopyFrom(IAnalysisSet[] args, DictionaryInfo res) {
            DictionaryInfo copied = args[0] as DictionaryInfo;
            if (copied != null) {
                res.CopyFrom(copied);
            }
        }
    }
}
