using System.Linq;
using Microsoft.PythonTools.Interpreter;
using Microsoft.PythonTools.Parsing.Ast;

namespace Microsoft.PythonTools.Analysis.Values {
    /// <summary>
    /// Represents a list object with tracked type information.
    /// </summary>
    class ListInfo : SequenceInfo {
        private AnalysisValue _appendMethod, _popMethod, _insertMethod, _extendMethod;

        public ListInfo(VariableDef[] indexTypes, BuiltinClassInfo seqType, Node node, ProjectEntry entry)
            : base(indexTypes, seqType, node, entry) {
        }

        public override IAnalysisSet GetMember(Node node, AnalysisUnit unit, string name) {
            // Must unconditionally call the base implementation of GetMember
            var res = base.GetMember(node, unit, name);

            switch (name) {
                case "append":
                    return _appendMethod = _appendMethod ?? new SpecializedCallable(
                        res.OfType<BuiltinNamespace<IPythonType>>().FirstOrDefault(),
                        ListAppend,
                        false
                    );
                case "pop":
                    return _popMethod = _popMethod ?? new SpecializedCallable(
                        res.OfType<BuiltinNamespace<IPythonType>>().FirstOrDefault(),
                        ListPop,
                        false
                    );
                case "insert":
                    return _insertMethod = _insertMethod ?? new SpecializedCallable(
                        res.OfType<BuiltinNamespace<IPythonType>>().FirstOrDefault(),
                        ListInsert,
                        false
                    );
                case "extend":
                    return _extendMethod = _extendMethod ?? new SpecializedCallable(
                        res.OfType<BuiltinNamespace<IPythonType>>().FirstOrDefault(),
                        ListExtend,
                        false
                    );
            }

            return res;
        }

        private IAnalysisSet ListAppend(Node node, AnalysisUnit unit, IAnalysisSet[] args, NameExpression[] keywordArgNames) {
            if (args.Length == 1) {
                AppendItem(node, unit, args[0]);
            }

            return unit.ProjectState._noneInst;
        }

        private IAnalysisSet ListPop(Node node, AnalysisUnit unit, IAnalysisSet[] args, NameExpression[] keywordArgNames) {
            return UnionType;
        }

        private IAnalysisSet ListInsert(Node node, AnalysisUnit unit, IAnalysisSet[] args, NameExpression[] keywordArgNames) {
            if (args.Length == 2) {
                AppendItem(node, unit, args[1]);
            }

            return unit.ProjectState._noneInst;
        }

        private IAnalysisSet ListExtend(Node node, AnalysisUnit unit, IAnalysisSet[] args, NameExpression[] keywordArgNames) {
            if (args.Length == 1) {
                foreach (var type in args[0]) {
                    AppendItem(node, unit, type.GetEnumeratorTypes(node, unit));
                }
            }

            return unit.ProjectState._noneInst;
        }

        private void AppendItem(Node node, AnalysisUnit unit, IAnalysisSet set) {
            if (IndexTypes.Length == 0) {
                IndexTypes = new[] { new VariableDef() };
            }

            IndexTypes[0].MakeUnionStrongerIfMoreThan(ProjectState.Limits.IndexTypes, set);
            IndexTypes[0].AddTypes(unit, set, true, DeclaringModule);

            UnionType = null;
        }
    }
}
