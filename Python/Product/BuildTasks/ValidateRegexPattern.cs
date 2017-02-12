using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace Microsoft.PythonTools.BuildTasks {
    /// <summary>
    /// Checks whether a pattern is valid regex.
    /// </summary>
    public class ValidateRegexPattern : Task {
        /// <summary>
        /// The pattern to validate.
        /// </summary>
        [Required]
        public string Pattern { get; set; }

        /// <summary>
        /// The message to display if the pattern is invalid. If the message
        /// contains "{0}", it will be replaced with the exception message.
        /// 
        /// If the message is empty, no error will be raised.
        /// </summary>
        public string Message { get; set; }

        [Output]
        public string IsValid { get; private set; }

        public override bool Execute() {
            try {
                var regex = new Regex(Pattern);
                IsValid = bool.TrueString;
            } catch (ArgumentException ex) {
                IsValid = bool.FalseString;
                if (!string.IsNullOrEmpty(Message)) {
                    Log.LogError(string.Format(Message, ex.Message));
                }
            }
            return !Log.HasLoggedErrors;
        }
    }
}
