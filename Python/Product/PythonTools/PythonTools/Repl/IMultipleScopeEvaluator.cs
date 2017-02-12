using System;
using System.Collections.Generic;

namespace Microsoft.PythonTools.Repl {
    interface IMultipleScopeEvaluator {
        /// <summary>
        /// Sets the current scope to the given name.
        /// </summary>
        void SetScope(string scopeName);

        /// <summary>
        /// Gets the list of scopes which can be changed to.
        /// </summary>
        IEnumerable<string> GetAvailableScopes();

        /// <summary>
        /// Gets the current scope name.
        /// </summary>
        string CurrentScopeName { get; }

        /// <summary>
        /// Gets the path to the file that defines the current scope. May be
        /// null if no file exists.
        /// </summary>
        string CurrentScopePath { get; }

        /// <summary>
        /// Event is fired when the list of available scopes changes.
        /// </summary>
        event EventHandler<EventArgs> AvailableScopesChanged;

        /// <summary>
        /// Event is fired when support of multiple scopes has changed.
        /// </summary>
        event EventHandler<EventArgs> MultipleScopeSupportChanged;

        /// <summary>
        /// Returns true if multiple scope support is currently enabled, false if not.
        /// </summary>
        bool EnableMultipleScopes {
            get;
        }
    }
}
