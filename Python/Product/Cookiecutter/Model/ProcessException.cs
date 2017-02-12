using System;
using System.Globalization;

namespace Microsoft.CookiecutterTools.Model {
    [Serializable]
    class ProcessException : Exception {
        public ProcessOutputResult Result { get; }

        public ProcessException(ProcessOutputResult result) :
            base(string.Format(CultureInfo.CurrentUICulture, Strings.ProcessExitCodeMessage, result.ExeFileName, result.ExitCode)) {
            Result = result;
        }
    }
}
