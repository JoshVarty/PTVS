using System.ComponentModel.Composition;

namespace Microsoft.PythonTools.Interpreter {
    /// <summary>
    /// Allows global (process-wide) options to be set for all interpreters.
    /// 
    /// This is intended primarily for the analyzer process. Most code should
    /// never set these options and should only read them.
    /// </summary>
    public static class GlobalInterpreterOptions {
        /// <summary>
        /// When True, factories should not watch the file system.
        /// </summary>
        public static bool SuppressFileSystemWatchers { get; set; }

        /// <summary>
        /// When True, factories should never provide a package manager.
        /// </summary>
        public static bool SuppressPackageManagers { get; set; }
    }
}
