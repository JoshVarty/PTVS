using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.PythonTools.Analysis;
using Microsoft.PythonTools.Parsing;
using Microsoft.PythonTools.Parsing.Ast;

namespace Microsoft.PythonTools.Interpreter.Ast {
    public sealed class AstPythonModule : IPythonModule, IProjectEntry, ILocatedMember {
        private readonly Dictionary<object, object> _properties;
        private readonly List<string> _childModules;
        private readonly Dictionary<string, IMember> _members;

        public static IPythonModule FromFile(IPythonInterpreter interpreter, string sourceFile, PythonLanguageVersion langVersion) {
            using (var stream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
                return FromStream(interpreter, stream, sourceFile, langVersion);
            }
        }

        public static IPythonModule FromStream(
            IPythonInterpreter interpreter,
            Stream sourceFile,
            string fileName,
            PythonLanguageVersion langVersion
        ) {
            PythonAst ast;
            using (var parser = Parser.CreateParser(sourceFile, langVersion)) {
                ast = parser.ParseFile();
            }

            return new AstPythonModule(interpreter, ast, fileName);
        }

        internal AstPythonModule() {
            Name = string.Empty;
            Documentation = string.Empty;
            FilePath = string.Empty;
            _properties = new Dictionary<object, object>();
            _childModules = new List<string>();
            _members = new Dictionary<string, IMember>();
        }

        internal AstPythonModule(IPythonInterpreter interpreter, PythonAst ast, string filePath) {
            Name = ast.Name;
            Documentation = ast.Documentation;
            FilePath = filePath;
            Locations = new[] { new LocationInfo(filePath, 1, 1) };

            _properties = new Dictionary<object, object>();
            _childModules = new List<string>();
            _members = new Dictionary<string, IMember>();

            var walker = new AstAnalysisWalker(interpreter, ast, this, filePath, _members);
            ast.Walk(walker);
        }

        internal void AddChildModule(string name, IPythonModule module) {
            lock (_childModules) {
                _childModules.Add(name);
            }
            lock (_members) {
                _members[name] = module;
            }
        }

        public string Name { get; }
        public string Documentation { get; }
        public string FilePath { get; }
        public PythonMemberType MemberType => PythonMemberType.Module;
        public Dictionary<object, object> Properties => _properties;
        public IEnumerable<LocationInfo> Locations { get; }

        public int AnalysisVersion => 1;
        public IModuleContext AnalysisContext => null;
        public bool IsAnalyzed => true;
        public void Analyze(CancellationToken cancel) { }

        public IEnumerable<string> GetChildrenModules() {
            lock (_childModules) {
                return _childModules.ToArray();
            }
        }

        public IMember GetMember(IModuleContext context, string name) {
            IMember member = null;
            lock (_members) {
                _members.TryGetValue(name, out member);
            }
            return member;
        }

        public IEnumerable<string> GetMemberNames(IModuleContext moduleContext) {
            lock (_members) {
                return _members.Keys.ToArray();
            }
        }

        public void Imported(IModuleContext context) { }
        public void RemovedFromProject() { }
    }
}
