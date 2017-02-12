using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.PythonTools.Interpreter;

namespace Microsoft.PythonTools.Analysis.Browser {
    class ClassView : MemberView {
        readonly IPythonType _type;
        
        public ClassView(IModuleContext context, string name, IPythonType member)
            : base(context, name, member) {
            _type = member;
        }
        
        public string OriginalName {
            get { return _type.Name; }
        }

        public override string SortKey {
            get { return "1"; }
        }

        public override string DisplayType {
            get { return "Type"; }
        }

        public override IEnumerable<KeyValuePair<string, object>> Properties {
            get {
                foreach (var p in base.Properties) {
                    yield return p;
                }

                yield return new KeyValuePair<string, object>("Original Name", OriginalName);

                int i = 1;
                var mro = _type.Mro;
                if (mro != null) {
                    foreach (var c in mro) {
                        yield return new KeyValuePair<string, object>(string.Format("MRO #{0}", i++), MemberView.Make(_context, c == null ? "(null)" : c.Name, c));
                    }
                }
            }
        }

        public override void ExportToTree(
            TextWriter writer,
            string currentIndent,
            string indent,
            out IEnumerable<IAnalysisItemView> exportChildren
        ) {
            writer.WriteLine("{0}class {1}", currentIndent, Name);
            exportChildren = SortedChildren;
        }

        private static string NameFromStack(Stack<IAnalysisItemView> stack) {
            return string.Join(".", stack.Select(av => av.Name));
        }

        public override void ExportToDiffable(
            TextWriter writer,
            string currentIndent,
            string indent,
            Stack<IAnalysisItemView> exportStack,
            out IEnumerable<IAnalysisItemView> exportChildren
        ) {
            writer.WriteLine("{0}{2} ({1})", currentIndent, DisplayType, Name);
            exportChildren = null;

            // Exclude members from test classes
            if (_type.Mro?.Any(t => t?.Name?.EndsWith(".TestCase") ?? false) ?? false) {
                return;
            }

            if (exportStack.Count == 0 || NameFromStack(exportStack) == _type.DeclaringModule.Name) {
                exportChildren = Children.OrderBy(c => c.Name);
            }
        }
    }
}
