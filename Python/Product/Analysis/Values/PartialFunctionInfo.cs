using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.PythonTools.Interpreter;
using Microsoft.PythonTools.Parsing.Ast;

namespace Microsoft.PythonTools.Analysis.Values {
    class PartialFunctionInfo : AnalysisValue {
        private readonly IAnalysisSet _function;
        private readonly IAnalysisSet[] _args;
        private readonly NameExpression[] _keywordArgNames;
        private readonly IPythonProjectEntry _declProjEntry;
        private IAnalysisSet _argsTuple;
        private IAnalysisSet _keywordsDict;

        public PartialFunctionInfo(ProjectEntry declProjEntry, IAnalysisSet function, IAnalysisSet[] args, NameExpression[] keywordArgNames) {
            _declProjEntry = declProjEntry;
            _function = function;
            _args = args;
            _keywordArgNames = keywordArgNames;
        }

        public override IPythonProjectEntry DeclaringModule {
            get {
                return _declProjEntry;
            }
        }

        public override IAnalysisSet Call(Node node, AnalysisUnit unit, IAnalysisSet[] args, NameExpression[] keywordArgNames) {
            var newArgs = _args.Take(_args.Length - _keywordArgNames.Length)
                .Concat(args.Take(args.Length - keywordArgNames.Length))
                .Concat(_args.Skip(_args.Length - _keywordArgNames.Length))
                .Concat(args.Skip(args.Length - keywordArgNames.Length))
                .ToArray();

            var newKwArgs = _keywordArgNames.Concat(keywordArgNames).ToArray();

            return _function.Call(node, unit, newArgs, newKwArgs);
        }

        public override IEnumerable<OverloadResult> Overloads {
            get {
                int skipCount = _args.Length - _keywordArgNames.Length;
                var skipNames = new HashSet<string>(_keywordArgNames.Select(n => n.Name));

                foreach (var overload in _function.SelectMany(f => f.Overloads)) {
                    yield return overload.WithNewParameters(
                        overload.Parameters.Skip(skipCount).Where(p => !skipNames.Contains(p.Name)).ToArray()
                    );
                }
            }
        }

        public override string ShortDescription {
            get {
                return "partial";
            }
        }

        public override string Description {
            get {
                var sb = new StringBuilder();
                sb.Append(ShortDescription);
                sb.Append('(');

                if (_function.Count == 1) {
                    sb.Append(_function.First().ToString());
                } else {
                    sb.Append('{');
                    sb.Append(string.Join(", ", _function));
                    sb.Append('}');
                }

                for (int i = 0; i < _args.Length; ++i) {
                    sb.Append(", ");
                    int j = i - _args.Length + _keywordArgNames.Length;
                    if (j >= 0) {
                        sb.Append(_keywordArgNames[j].Name);
                        sb.Append('=');
                    }
                    var arg = _args[i];
                    if (arg.Count == 1) {
                        sb.Append(arg.First().ToString());
                    } else {
                        sb.Append('{');
                        sb.Append(string.Join(", ", arg));
                        sb.Append('}');
                    }
                }

                sb.Append(")");
                return sb.ToString();
            }
        }

        public override IAnalysisSet GetMember(Node node, AnalysisUnit unit, string name) {
            var res = AnalysisSet.Empty;

            if (name == "func") {
                AddReference(node, unit);
                return _function;
            } else if (name == "args") {
                AddReference(node, unit);
                if (_argsTuple == null) {
                    _argsTuple = new SequenceInfo(_args.Take(_args.Length - _keywordArgNames.Length)
                        .Select(v => {
                            var vd = new VariableDef();
                            vd.AddTypes(unit, v, false, DeclaringModule);
                            return vd;
                        }).ToArray(),
                        unit.ProjectState.ClassInfos[BuiltinTypeId.Tuple],
                        node,
                        unit.ProjectEntry
                    );
                }
                return _argsTuple;
            } else if (name == "keywords") {
                AddReference(node, unit);
                if (_keywordsDict == null) {
                    var dict = new DictionaryInfo(unit.ProjectEntry, node);
                    _keywordsDict = dict;
                    for (int i = 0; i < _keywordArgNames.Length; ++i) {
                        int j = i + _args.Length - _keywordArgNames.Length;
                        if (j >= 0 && j < _args.Length) {
                            dict._keysAndValues.AddTypes(
                                unit,
                                unit.ProjectState.GetConstant(_keywordArgNames[i].Name),
                                _args[j],
                                false
                            );
                        }
                    }
                }
                return _keywordsDict;
            }
            return res;
        }
    }
}
