using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.PythonTools.Interpreter;

namespace Microsoft.PythonTools.Analysis.Browser {
    class ModuleRefView : MemberView {
        public ModuleRefView(IModuleContext context, string name, IPythonModule module)
            : base(context, name, module) {
        }

        public override string SortKey {
            get { return "8"; }
        }

        public override string DisplayType {
            get { return "Module reference"; }
        }

        public override void ExportToTree(
            TextWriter writer,
            string currentIndent,
            string indent,
            out IEnumerable<IAnalysisItemView> exportChildren
        ) {
            writer.WriteLine("{0}import {1}", currentIndent, Name);
            exportChildren = null;
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
        }
    }
}
