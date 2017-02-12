using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.PythonTools.Interpreter;

namespace Microsoft.PythonTools.Analysis.Browser {
    class PropertyView : MemberView {
        readonly IBuiltinProperty _value;

        public PropertyView(IModuleContext context, string name, IBuiltinProperty member)
            : base(context, name, member) {
            _value = member;
        }

        public override string SortKey {
            get { return "4"; }
        }

        public override string DisplayType {
            get { return "Property"; }
        }

        public override IEnumerable<IAnalysisItemView> Children {
            get {
                if (_value != null && _value.Type != null) {
                    yield return MemberView.Make(_context, _value.Type.Name, _value.Type);
                }
            }
        }

        public override void ExportToTree(
            TextWriter writer,
            string currentIndent,
            string indent,
            out IEnumerable<IAnalysisItemView> exportChildren
        ) {
            writer.WriteLine("{0}{1}: {2} = {3}", currentIndent, DisplayType, Name, _value.Type == null ? "(null)" : _value.Type.Name);
            exportChildren = null;
        }
    }
}
