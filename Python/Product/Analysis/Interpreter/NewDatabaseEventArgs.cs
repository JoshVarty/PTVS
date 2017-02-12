using System;

namespace Microsoft.PythonTools.Interpreter {
    /// <summary>
    /// The data passed in the <see cref="PythonTypeDatabase.DatabaseReplaced"/>
    /// event.
    /// </summary>
    public class DatabaseReplacedEventArgs : EventArgs {
        readonly PythonTypeDatabase _newDatabase;

        public DatabaseReplacedEventArgs(PythonTypeDatabase newDatabase) {
            _newDatabase = newDatabase;
        }

        /// <summary>
        /// The updated database.
        /// </summary>
        public PythonTypeDatabase NewDatabase {
            get {
                return _newDatabase;
            }
        }
    }
}
