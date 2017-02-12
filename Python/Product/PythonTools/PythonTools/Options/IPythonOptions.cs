using System;
using System.Runtime.InteropServices;
using Microsoft.PythonTools.Parsing;

namespace Microsoft.PythonTools.Options {
    // TODO: We should switch to a scheme which takes strings / returns object for options so they're extensible w/o reving the interface
    [Guid("BACA2500-5EA7-4075-8D02-647EAC0BC6E3")]
    public interface IPythonOptions {
        IPythonIntellisenseOptions Intellisense { get; }

        IPythonInteractiveOptions Interactive { get; }

        bool PromptBeforeRunningWithBuildErrorSetting {
            get;
            set;
        }

        bool AutoAnalyzeStandardLibrary {
            get;
            set;
        }

        Severity IndentationInconsistencySeverity {
            get;
            set;
        }

        bool TeeStandardOutput {
            get;
            set;
        }

        bool WaitOnAbnormalExit {
            get;
            set;
        }

        bool WaitOnNormalExit {
            get;
            set;
        }
    }

    [Guid("77179244-BBD7-4AA2-B27B-F2CCC679953A")]
    public interface IPythonIntellisenseOptions {
        bool AddNewLineAtEndOfFullyTypedWord { get; set; }
        bool EnterCommitsCompletion { get; set; }
        bool UseMemberIntersection { get; set; }
        string CompletionCommittedBy { get; set; }
        bool AutoListIdentifiers { get; set; }
    }

    [Guid("28214322-2EEC-4750-8D87-EF76714757CE")]
    public interface IPythonInteractiveOptions {
        bool UseSmartHistory {
            get;
            set;
        }

        string CompletionMode {
            get;
            set;
        }

        string StartupScripts {
            get;
            set;
        }
    }
}
