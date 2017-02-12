using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.PythonTools.Infrastructure {
    public static class ExceptionExtensions {
        /// <summary>
        /// Returns true if an exception should not be handled by logging code.
        /// </summary>
        public static bool IsCriticalException(this Exception ex) {
            return ex is StackOverflowException ||
                ex is OutOfMemoryException ||
                ex is ThreadAbortException ||
                ex is AccessViolationException ||
                ex is CriticalException;
        }

        public static string ToUnhandledExceptionMessage(
            this Exception ex,
            Type callerType,
            [CallerFilePath] string callerFile = null,
            [CallerLineNumber] int callerLineNumber = 0,
            [CallerMemberName] string callerName = null
        ) {
            if (string.IsNullOrEmpty(callerName)) {
                callerName = callerType != null ? callerType.FullName : string.Empty;
            } else if (callerType != null) {
                callerName = callerType.FullName + "." + callerName;
            }

            try {
                return string.Format(
                    Strings.UnhandledException,
                    ex,
                    callerFile ?? String.Empty,
                    callerLineNumber,
                    callerName
                );
            } catch (Exception ex2) {
                // Never throw out of this function.
                try {
                    return ex2.ToString();
                } catch (Exception) {
                    // Never -- NEVER -- throw out of this function.
                    return "Unhandled exception constructing exception message.";
                }
            }
        }

    }

    /// <summary>
    /// An exception that should not be silently handled and logged.
    /// </summary>
    [Serializable]
    public class CriticalException : Exception {
        public CriticalException() { }
        public CriticalException(string message) : base(message) { }
        public CriticalException(string message, Exception inner) : base(message, inner) { }
        protected CriticalException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}