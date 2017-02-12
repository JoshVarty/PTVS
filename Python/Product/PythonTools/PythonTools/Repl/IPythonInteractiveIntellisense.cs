using System.Collections.Generic;
using Microsoft.PythonTools.Intellisense;

namespace Microsoft.PythonTools.Repl {
    interface IPythonInteractiveIntellisense {
        bool LiveCompletionsOnly { get; }
        IEnumerable<KeyValuePair<string, string>> GetAvailableScopesAndPaths();
        CompletionResult[] GetMemberNames(string text);
        OverloadDoc[] GetSignatureDocumentation(string text);
        VsProjectAnalyzer Analyzer { get; }
        string AnalysisFilename { get; }
    }
}
