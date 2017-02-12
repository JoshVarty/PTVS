using System;
using System.Collections.Generic;

namespace Microsoft.PythonTools.Parsing {
    public class CollectingErrorSink  : ErrorSink {
        private readonly List<ErrorResult> _errors = new List<ErrorResult>();
        private readonly List<ErrorResult> _warnings = new List<ErrorResult>();

        public override void Add(string message, NewLineLocation[] lineLocations, int startIndex, int endIndex, int errorCode, Severity severity) {
            if (severity == Severity.Error || severity == Severity.FatalError) {
                _errors.Add(new ErrorResult(message, new SourceSpan(NewLineLocation.IndexToLocation(lineLocations, startIndex), NewLineLocation.IndexToLocation(lineLocations, endIndex))));
            } else if (severity == Severity.Warning) {
                _warnings.Add(new ErrorResult(message, new SourceSpan(NewLineLocation.IndexToLocation(lineLocations, startIndex), NewLineLocation.IndexToLocation(lineLocations, endIndex))));
            }
        }

        public List<ErrorResult> Errors {
            get {
                return _errors;
            }
        }

        public List<ErrorResult> Warnings {
            get {
                return _warnings;
            }
        }
    }
}
