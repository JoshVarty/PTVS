using System.Collections.Generic;
using System.Linq;
using Microsoft.PythonTools.Django.Analysis;
using Microsoft.PythonTools.Django.TemplateParsing;
using Microsoft.PythonTools.Intellisense;
using Microsoft.PythonTools.Interpreter;

namespace Microsoft.PythonTools.Django.Intellisense {
    internal class ProjectBlockCompletionContextBase : IDjangoCompletionContext {
        private readonly VsProjectAnalyzer _analyzer;
        private readonly string _filename;
        private HashSet<string> _loopVars;

        public ProjectBlockCompletionContextBase(VsProjectAnalyzer analyzer, string filename) {
            _analyzer = analyzer;
            _filename = filename;
        }

        protected void AddLoopVariable(string name) {
            if (_loopVars == null) {
                _loopVars = new HashSet<string>();
            }
            _loopVars.Add(name);
        }

        public string[] Variables {
            get {
                var res = _analyzer.GetVariableNames(_filename);
                if (_loopVars != null) {
                    HashSet<string> tmp = new HashSet<string>(res);

                    tmp.UnionWith(_loopVars);
                    return tmp.ToArray();
                }
                return res;
            }
        }

        public Dictionary<string, TagInfo> Filters {
            get {
                return _analyzer.GetFilters();
            }
        }

        public DjangoUrl[] Urls {
            get {
                return _analyzer.GetUrls();
            }
        }

        public Dictionary<string, PythonMemberType> GetMembers(string name) {
            return _analyzer.GetMembers(_filename, name);
        }
    }
}
