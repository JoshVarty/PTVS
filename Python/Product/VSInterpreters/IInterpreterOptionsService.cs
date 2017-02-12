using System;

namespace Microsoft.PythonTools.Interpreter {

    /// <summary>
    /// Provides information about the available interpreters and the current
    /// default. Instances of this service should be obtained using MEF.
    /// </summary>
    public interface IInterpreterOptionsService {
        /// <summary>
        /// Gets or sets the default interpreter.
        /// </summary>
        IPythonInterpreterFactory DefaultInterpreter { get; set; }

        string DefaultInterpreterId { get; set; }

        /// <summary>
        /// Raised when the default interpreter is set to a new value.
        /// </summary>
        event EventHandler DefaultInterpreterChanged;

        /// <summary>
        /// Adds a new user configured interpreter factory to the registry stored
        /// under the provided name.  The id in the configuration is ignored and 
        /// the newly registered id is returned.
        /// </summary>
        string AddConfigurableInterpreter(string name, InterpreterConfiguration config);

        void RemoveConfigurableInterpreter(string id);
        bool IsConfigurable(string id);
    }
}
