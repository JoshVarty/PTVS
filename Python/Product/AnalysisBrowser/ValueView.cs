using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.PythonTools.Interpreter;

namespace Microsoft.PythonTools.Analysis.Browser {
    class ValueView : MemberView {
        readonly IPythonConstant _value;
        IAnalysisItemView _type;

        public ValueView(IModuleContext context, string name, IPythonConstant member)
            : base(context, name, member) {
            _value = member;
        }

        public override string SortKey {
            get { return "3"; }
        }

        public override string DisplayType {
            get { return "Constant"; }
        }

        public IAnalysisItemView Type {
            get {
                if (_value != null && _value.Type != null && _type == null) {
                    _type = MemberView.Make(_context, _value.Type.Name, _value.Type);
                }
                return _type;
            }
        }

        public override IEnumerable<KeyValuePair<string, object>> Properties {
            get {
                foreach (var p in base.Properties) {
                    yield return p;
                }

                if (Type != null) {
                    yield return new KeyValuePair<string, object>("Type", Type);
                }
            }
        }

        public override void ExportToTree(
            TextWriter writer,
            string currentIndent,
            string indent,
            out IEnumerable<IAnalysisItemView> exportChildren
        ) {
            writer.WriteLine("{0}{1}: {2} = {3}", currentIndent, DisplayType, Name, _value.Type.Name);
            exportChildren = null;
        }

        public override void ExportToDiffable(
            TextWriter writer,
            string currentIndent,
            string indent,
            Stack<IAnalysisItemView> exportStack,
            out IEnumerable<IAnalysisItemView> exportChildren
        ) {
            writer.WriteLine("{0}{2} = {3} ({1})", currentIndent, DisplayType, Name, _value.Type.Name);
            exportChildren = null;
        }
    }
}
