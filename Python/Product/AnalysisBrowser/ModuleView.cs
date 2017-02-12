using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.PythonTools.Interpreter;
using Microsoft.PythonTools.Interpreter.Default;

namespace Microsoft.PythonTools.Analysis.Browser {
    class ModuleView : IAnalysisItemView {
        readonly IPythonInterpreter _interpreter;
        readonly IModuleContext _context;
        readonly string _idbPath;
        readonly IEnumerable<IAnalysisItemView> _children;
        IPythonModule _module;

        public ModuleView(IPythonInterpreter interpreter, IModuleContext context, string name, string idbPath) {
            _interpreter = interpreter;
            _context = context;
            Name = name;
            _idbPath = idbPath;
            _children = CalculateChildren().ToArray();
        }

        private IEnumerable<IAnalysisItemView> CalculateChildren() {
            if (_module == null) {
                _module = _interpreter.ImportModule(Name);
            }

            if (File.Exists(_idbPath)) {
                yield return RawView.FromFile(_idbPath);
            }

            var cpm = _module as CPythonModule;
            if (cpm != null) {
                cpm.EnsureLoaded();
            }

            if (cpm != null && cpm._hiddenMembers != null) {
                foreach (var keyValue in cpm._hiddenMembers) {
                    yield return MemberView.Make(_context, keyValue.Key, keyValue.Value);
                }
            }

            foreach (var memberName in _module.GetMemberNames(_context)) {
                yield return MemberView.Make(_context, _module, memberName);
            }
        }

        public string Name { get; private set; }

        public string SortKey { get { return "0"; } }

        public string DisplayType {
            get { return "Module"; }
        }

        public override string ToString() {
            return Name;
        }

        public IEnumerable<IAnalysisItemView> Children {
            get {
                return _children;
            }
        }

        public IEnumerable<IAnalysisItemView> SortedChildren {
            get { return Children.OrderBy(c => c.SortKey).ThenBy(c => c.Name); }
        }

        public void ExportToTree(
            TextWriter writer,
            string currentIndent,
            string indent,
            out IEnumerable<IAnalysisItemView> exportChildren
        ) {
            writer.WriteLine("{0}{1}: {2}", currentIndent, DisplayType, Name);
            exportChildren = SortedChildren;
        }

        public void ExportToDiffable(
            TextWriter writer,
            string currentIndent,
            string indent,
            Stack<IAnalysisItemView> exportStack,
            out IEnumerable<IAnalysisItemView> exportChildren
        ) {
            writer.WriteLine("{0}{2} ({1})", currentIndent, DisplayType, Name);
            exportChildren = Children.OrderBy(c => c.Name);
        }

        public string SourceLocation {
            get {
                var entry = _module as IProjectEntry;
                if (entry != null) {
                    return entry.FilePath;
                }
                return "No location";
            }
        }

        public IEnumerable<KeyValuePair<string, object>> Properties {
            get {
                yield return new KeyValuePair<string, object>("Location", SourceLocation);
            }
        }
    }
}
