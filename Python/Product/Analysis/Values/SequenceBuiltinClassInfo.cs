using System.Collections.Generic;
using System.Linq;
using Microsoft.PythonTools.Analysis.Analyzer;
using Microsoft.PythonTools.Interpreter;
using Microsoft.PythonTools.Parsing.Ast;

namespace Microsoft.PythonTools.Analysis.Values {
    /// <summary>
    /// Specialized ClassInfo for sequence types.
    /// </summary>
    abstract class SequenceBuiltinClassInfo : BuiltinClassInfo {
        protected readonly IAnalysisSet _indexTypes;

        public SequenceBuiltinClassInfo(IPythonType classObj, PythonAnalyzer projectState)
            : base(classObj, projectState) {
            var seqType = classObj as IPythonSequenceType;
            if (seqType != null && seqType.IndexTypes != null) {
                _indexTypes = projectState.GetAnalysisSetFromObjects(seqType.IndexTypes).GetInstanceType();
            } else {
                _indexTypes = AnalysisSet.Empty;
            }
        }

        internal IAnalysisSet IndexTypes {
            get { return _indexTypes; }
        }

        public override IAnalysisSet Call(Node node, AnalysisUnit unit, IAnalysisSet[] args, NameExpression[] keywordArgNames) {
            if (args.Length == 1) {
                var res = unit.Scope.GetOrMakeNodeValue(
                    node,
                    NodeValueKind.Sequence,
                    (node_) => MakeFromIndexes(node_, unit.ProjectEntry)
                ) as SequenceInfo;

                List<IAnalysisSet> seqTypes = new List<IAnalysisSet>();
                foreach (var type in args[0]) {
                    SequenceInfo seqInfo = type as SequenceInfo;
                    if (seqInfo != null) {
                        for (int i = 0; i < seqInfo.IndexTypes.Length; i++) {
                            if (seqTypes.Count == i) {
                                seqTypes.Add(seqInfo.IndexTypes[i].Types);
                            } else {
                                seqTypes[i] = seqTypes[i].Union(seqInfo.IndexTypes[i].Types);
                            }
                        }
                    } else {
                        var defaultIndexType = type.GetIndex(node, unit, ProjectState.GetConstant(0));
                        if (seqTypes.Count == 0) {
                            seqTypes.Add(defaultIndexType);
                        } else {
                            seqTypes[0] = seqTypes[0].Union(defaultIndexType);
                        }
                    }
                }

                res.AddTypes(unit, seqTypes.ToArray());

                return res;
            }

            return base.Call(node, unit, args, keywordArgNames);
        }

        private static string GetInstanceShortDescription(AnalysisValue ns) {
            var bci = ns as BuiltinClassInfo;
            if (bci != null) {
                return bci.Instance.ShortDescription;
            }
            return ns.ShortDescription;
        }

        protected string MakeDescription(string typeName) {
            if (_indexTypes == null || _indexTypes.Count == 0) {
                return typeName;
            } else if (_indexTypes.Count == 1) {
                return typeName + " of " + GetInstanceShortDescription(_indexTypes.First());
            } else if (_indexTypes.Count < 4) {
                return typeName + " of {" + string.Join(", ", _indexTypes.Select(GetInstanceShortDescription)) + "}";
            } else {
                return typeName + " of multiple types";
            }
        }

        public override string ShortDescription {
            get {
                return MakeDescription(_type.Name);
            }
        }

        internal override int UnionHashCode(int strength) {
            if (strength < MergeStrength.ToObject && _indexTypes.Any()) {
                var type = ProjectState.ClassInfos[TypeId];
                if (type != null) {
                    // Use our unspecialized type's hash code.
                    return type.UnionHashCode(strength);
                }
            }
            return base.UnionHashCode(strength);
        }

        internal abstract SequenceInfo MakeFromIndexes(Node node, ProjectEntry entry);
    }
}
