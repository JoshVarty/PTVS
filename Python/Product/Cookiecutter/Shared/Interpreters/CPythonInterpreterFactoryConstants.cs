using System;
using System.Text.RegularExpressions;

namespace Microsoft.CookiecutterTools.Interpreters {
    /// <summary>
    /// Provides constants used to identify interpreters that are detected from
    /// the CPython registry settings.
    /// 
    /// This class used by Microsoft.PythonTools.dll to register the
    /// interpreters.
    /// </summary>
    public static class CPythonInterpreterFactoryConstants {
        public const string ConsoleExecutable = "python.exe";
        public const string WindowsExecutable = "pythonw.exe";
        public const string LibrarySubPath = "lib";
        public const string PathEnvironmentVariableName = "PYTHONPATH";

        private static readonly Regex IdParser = new Regex(
            "^(?<provider>.+?)\\|(?<company>.+?)\\|(?<tag>.+?)$",
            RegexOptions.None,
            TimeSpan.FromSeconds(1)
        );

        public static string GetInterpreterId(string company, string tag) {
            return String.Join(
                "|", 
                CPythonInterpreterFactoryProvider.FactoryProviderName,
                company,
                tag
            );
        }

        public static bool TryParseInterpreterId(string id, out string company, out string tag) {
            tag = company = null;
            try {
                var m = IdParser.Match(id);
                if (m.Success && m.Groups["provider"].Value == CPythonInterpreterFactoryProvider.FactoryProviderName) {
                    tag = m.Groups["tag"].Value;
                    company = m.Groups["company"].Value;
                    return true;
                }
                return false;
            } catch (RegexMatchTimeoutException) {
                return false;
            }
        }
    }
}
