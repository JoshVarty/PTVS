using System;
using Microsoft.PythonTools.Parsing;

namespace Microsoft.PythonTools.Interpreter {
    public static class AnalysisExtensions {
        public static PythonLanguageVersion GetLanguageVersion(this IPythonInterpreterFactory factory) {
            return factory.Configuration.Version.ToLanguageVersion();
        }

        /// <summary>
        /// Removes all trailing white space including new lines, tabs, and form feeds.
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string TrimDocumentation(this string self) {
            if (self != null) {
                return self.TrimEnd('\n', '\r', ' ', '\f', '\t');
            } 
            return self;
        }
    }
}
