using System;

namespace Microsoft.PythonTools.Interpreter {
    /// <summary>
    /// The options that may be passed to
    /// <see cref="IPythonInterpreterFactoryWithDatabase.GenerateDatabase"/>
    /// </summary>
    [Flags]
    public enum GenerateDatabaseOptions {
        /// <summary>
        /// Runs a full analysis for the interpreter's standard library and
        /// installed packages.
        /// </summary>
        None,
        /// <summary>
        /// Skips analysis if the modification time of every file in a package
        /// is earlier than the database's time. This option prefers false
        /// negatives (that is, analyze something that did not need it) if it is
        /// likely that the results could be outdated.
        /// </summary>
        SkipUnchanged
    }
}
