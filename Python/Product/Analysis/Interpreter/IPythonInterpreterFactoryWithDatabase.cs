using System;
using System.Collections.Generic;

namespace Microsoft.PythonTools.Interpreter {
    /// <summary>
    /// Implemented by Python interpreters which support generating a completion
    /// database.
    /// </summary>
    public interface IPythonInterpreterFactoryWithDatabase : IPythonInterpreterFactory {
        /// <summary>
        /// Generates the completion database.
        /// </summary>
        void GenerateDatabase(GenerateDatabaseOptions options, Action<int> onExit = null);

        /// <summary>
        /// Gets whether or not the completion database is currently up to date.
        /// </summary>
        /// <remarks>New in 1.1</remarks>
        bool IsCurrent {
            get;
        }

        /// <summary>
        /// Raised when the value of IsCurrent is refreshed.
        /// </summary>
        /// <remarks>New in 2.0</remarks>
        event EventHandler IsCurrentChanged;

        /// <summary>
        /// Returns logged information about the analysis of the interpreter's library.
        /// 
        /// May return null if no information is available, or a string containing error
        /// text if an error occurs.
        /// </summary>
        /// <remarks>New in 2.0</remarks>
        string GetAnalysisLogContent(IFormatProvider culture);

        /// <summary>
        /// Returns a string describing the reason why IsCurrent has its current
        /// value. The string is formatted for display according to the provided
        /// culture and may use localized resources if available.
        /// 
        /// May return null if no information is available, or a string
        /// containing error text if an error occurs.
        /// </summary>
        /// <remarks>New in 2.0</remarks>
        string GetFriendlyIsCurrentReason(IFormatProvider culture);

        /// <summary>
        /// Returns a string describing the reason why IsCurrent has its current
        /// value.
        /// 
        /// This string may not be suitable for displaying directly to the user.
        /// It is always formatted using the invariant culture, but may use
        /// resources localized to the provided culture if they are available.
        /// 
        /// May return null if no information is available, or a string
        /// containing detailed exception information if an error occurs.
        /// </summary>
        /// <remarks>New in 2.0</remarks>
        string GetIsCurrentReason(IFormatProvider culture);

        /// <summary>
        /// Gets whether the database is currently being checked to determine
        /// the correct value for <see cref="IsCurrent"/>. If database checking
        /// is instantaneous, this may always return false.
        /// </summary>
        /// <remarks>New in 2.0</remarks>
        bool IsCheckingDatabase { get; }

        /// <summary>
        /// Returns a list of module names that appear to have current entries
        /// in the database.
        /// </summary>
        IEnumerable<string> GetUpToDateModules();

        /// <summary>
        /// Raised when a new database is available. 
        /// </summary>
        event EventHandler NewDatabaseAvailable;
    }
}
