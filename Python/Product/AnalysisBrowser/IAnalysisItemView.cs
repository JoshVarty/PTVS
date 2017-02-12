using System.Collections.Generic;
using System.IO;

namespace Microsoft.PythonTools.Analysis.Browser {
    interface IAnalysisItemView {
        string Name { get; }
        string SortKey { get; }
        string DisplayType { get; }
        string SourceLocation { get; }
        IEnumerable<IAnalysisItemView> Children { get; }
        IEnumerable<IAnalysisItemView> SortedChildren { get; }
        IEnumerable<KeyValuePair<string, object>> Properties { get; }

        void ExportToTree(
            TextWriter writer,
            string currentIndent,
            string indent,
            out IEnumerable<IAnalysisItemView> exportChildren
        );

        void ExportToDiffable(
            TextWriter writer,
            string currentIndent,
            string indent,
            Stack<IAnalysisItemView> exportStack,
            out IEnumerable<IAnalysisItemView> exportChildren
        );
    }
}
