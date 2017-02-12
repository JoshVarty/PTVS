using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestWindow.Extensibility;

namespace Microsoft.PythonTools.TestAdapter {
    [Export(typeof(IStackTraceParser))]
    sealed class PythonStackTraceParser : IStackTraceParser {
        public Uri ExecutorUri {
            get {
                return TestContainerDiscoverer._ExecutorUri;
            }
        }

        public IEnumerable<StackFrame> GetStackFrames(string errorStackTrace) {
            var regex = new Regex(@"File ""(.+)"", line (\d+), in (\w+)");

            foreach (Match match in regex.Matches(errorStackTrace)) {
                int lineno;
                if (int.TryParse(match.Groups[2].Value, out lineno)) {
                    yield return new StackFrame(
                        match.Groups[3].Value,
                        match.Groups[1].Value,
                        lineno
                    );
                }
            }
        }
    }
}
