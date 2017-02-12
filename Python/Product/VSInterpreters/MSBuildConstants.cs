using System.Text.RegularExpressions;

namespace Microsoft.PythonTools.Interpreter {
    public static class MSBuildConstants {

        // keys used for storing information about user defined interpreters
        public const string InterpreterItem = "Interpreter";
        public const string IdKey = "Id";
        public const string InterpreterPathKey = "InterpreterPath";
        public const string WindowsPathKey = "WindowsInterpreterPath";
        public const string LibraryPathKey = "LibraryPath";
        public const string ArchitectureKey = "Architecture";
        public const string VersionKey = "Version";
        public const string PathEnvVarKey = "PathEnvironmentVariable";
        public const string DescriptionKey = "Description";
        public const string BaseInterpreterKey = "BaseInterpreter";

        public const string InterpreterReferenceItem = "InterpreterReference";
        private static readonly Regex InterpreterReferencePath = new Regex(
            @"\{?(?<id>[a-f0-9\-]+)\}?
              \\
              (?<version>[23]\.[0-9])",
            RegexOptions.CultureInvariant | RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase
        );

        public const string InterpreterIdProperty = "InterpreterId";
        internal const string InterpreterVersionProperty = "InterpreterVersion";
    }
}
