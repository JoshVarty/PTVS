using System;
using System.Collections.Generic;

namespace Microsoft.PythonTools.Interpreter {
    /// <summary>
    /// Provides information to PythonTypeDatabase on how to generate a
    /// database.
    /// </summary>
    public sealed class PythonTypeDatabaseCreationRequest {
        public PythonTypeDatabaseCreationRequest() {
            ExtraInputDatabases = new List<string>();
        }

        /// <summary>
        /// The interpreter factory to use. This will provide language version
        /// and source paths.
        /// </summary>
        public PythonInterpreterFactoryWithDatabase Factory { get; set; }

        /// <summary>
        /// A list of extra databases to load when analyzing the factory's
        /// library.
        /// </summary>
        public List<string> ExtraInputDatabases { get; private set; }

        /// <summary>
        /// The directory to write the database to.
        /// </summary>
        public string OutputPath { get; set; }

        /// <summary>
        /// True to avoid analyzing packages that are up to date; false to
        /// regenerate the entire database.
        /// </summary>
        public bool SkipUnchanged { get; set; }

        /// <summary>
        /// A factory to wait for before starting regeneration.
        /// </summary>
        public IPythonInterpreterFactoryWithDatabase WaitFor { get; set; }

        /// <summary>
        /// A function to call when the analysis process is completed. The value
        /// is an error code.
        /// </summary>
        public Action<int> OnExit { get; set; }
    }
}
