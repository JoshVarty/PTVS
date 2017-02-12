using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.PythonTools.Interpreter;

namespace Microsoft.PythonTools.Analysis.Browser {
    class ParameterView : IAnalysisItemView {
        readonly IModuleContext _context;
        readonly IParameterInfo _parameter;
        readonly Lazy<IEnumerable<IAnalysisItemView>> _types;
        
        public ParameterView(IModuleContext context, IParameterInfo parameter) {
            _context = context;
            _parameter = parameter;
            _types = new Lazy<IEnumerable<IAnalysisItemView>>(CalculateTypes);

            Name = _parameter.Name;
            if (_parameter.IsParamArray) {
                Name = "*" + Name;
            } else if (_parameter.IsKeywordDict) {
                Name = "**" + Name;
            }
        }

        private IEnumerable<IAnalysisItemView> CalculateTypes() {
            if (_parameter.ParameterTypes == null || _parameter.ParameterTypes.Count == 0) {
                return Enumerable.Empty<IAnalysisItemView>();
            }

            return _parameter.ParameterTypes
                .Select(t => MemberView.Make(_context, t.Name, t))
                .ToArray();
        }

        public string Name {
            get;
            private set;
        }

        public string SortKey {
            get {
                // Sort Key is irrelevant, since parameters are only ever
                // grouped with parametetrs.
                return "0";
            }
        }

        public string DisplayType {
            get { return "Parameter"; }
        }

        public string SourceLocation {
            get { return null; }
        }

        public IEnumerable<IAnalysisItemView> Types {
            get { return _types.Value; }
        }

        public IEnumerable<IAnalysisItemView> Children {
            get { return Enumerable.Empty<IAnalysisItemView>(); }
        }

        public IEnumerable<IAnalysisItemView> SortedChildren {
            get { return Enumerable.Empty<IAnalysisItemView>(); }
        }

        public IEnumerable<KeyValuePair<string, object>> Properties {
            get {
                if (!string.IsNullOrEmpty(_parameter.Documentation)) {
                    yield return new KeyValuePair<string, object>("__doc__", _parameter.Documentation);
                }
                if (!string.IsNullOrEmpty(_parameter.DefaultValue)) {
                    yield return new KeyValuePair<string, object>("Default", _parameter.DefaultValue);
                }

                int i = 0;
                foreach (var type in Types) {
                    yield return new KeyValuePair<string, object>(string.Format("#{0}", i++), type);
                }
            }
        }

        public void ExportToTree(
            TextWriter writer,
            string currentIndent,
            string indent,
            out IEnumerable<IAnalysisItemView> exportChildren
        ) {
            exportChildren = null;
        }

        public void ExportToDiffable(
            TextWriter writer,
            string currentIndent,
            string indent,
            Stack<IAnalysisItemView> exportStack,
            out IEnumerable<IAnalysisItemView> exportChildren
        ) {
            exportChildren = null;
        }
    }
}
