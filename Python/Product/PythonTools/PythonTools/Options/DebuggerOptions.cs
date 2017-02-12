using System;

namespace Microsoft.PythonTools.Options {
    public sealed class DebuggerOptions {
        private readonly PythonToolsService _service;

        private const string Category = "Advanced";

        private const string DontPromptBeforeRunningWithBuildErrorSetting = "DontPromptBeforeRunningWithBuildError";
        private const string WaitOnAbnormalExitSetting = "WaitOnAbnormalExit";
        private const string WaitOnNormalExitSetting = "WaitOnNormalExit";
        private const string TeeStandardOutSetting = "TeeStandardOut";
        private const string BreakOnSystemExitZeroSetting = "BreakOnSystemExitZero";
        private const string DebugStdLibSetting = "DebugStdLib";

        internal DebuggerOptions(PythonToolsService service) {
            _service = service;
            Load();
        }

        public void Load() {
            PromptBeforeRunningWithBuildError = !(_service.LoadBool(DontPromptBeforeRunningWithBuildErrorSetting, Category) ?? false);
            WaitOnAbnormalExit = _service.LoadBool(WaitOnAbnormalExitSetting, Category) ?? true;
            WaitOnNormalExit = _service.LoadBool(WaitOnNormalExitSetting, Category) ?? true;
            TeeStandardOutput = _service.LoadBool(TeeStandardOutSetting, Category) ?? true;
            BreakOnSystemExitZero = _service.LoadBool(BreakOnSystemExitZeroSetting, Category) ?? false;
            DebugStdLib = _service.LoadBool(DebugStdLibSetting, Category) ?? false;
            Changed?.Invoke(this, EventArgs.Empty);
        }

        public void Save() {
            _service.SaveBool(DontPromptBeforeRunningWithBuildErrorSetting, Category, !PromptBeforeRunningWithBuildError);
            _service.SaveBool(WaitOnAbnormalExitSetting, Category, WaitOnAbnormalExit);
            _service.SaveBool(WaitOnNormalExitSetting, Category, WaitOnNormalExit);
            _service.SaveBool(TeeStandardOutSetting, Category, TeeStandardOutput);
            _service.SaveBool(BreakOnSystemExitZeroSetting, Category, BreakOnSystemExitZero);
            _service.SaveBool(DebugStdLibSetting, Category, DebugStdLib);
            Changed?.Invoke(this, EventArgs.Empty);
        }

        public void Reset() {
            PromptBeforeRunningWithBuildError = false;
            WaitOnAbnormalExit = true;
            WaitOnNormalExit = true;
            TeeStandardOutput = true;
            BreakOnSystemExitZero = false;
            DebugStdLib = false;
            Changed?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler Changed;

        /// <summary>
        /// True to ask the user whether to run when their code contains errors.
        /// Default is false.
        /// </summary>
        public bool PromptBeforeRunningWithBuildError {
            get;
            set;
        }

        /// <summary>
        /// True to copy standard output from a Python process into the Output
        /// window. Default is true.
        /// </summary>
        public bool TeeStandardOutput {
            get;
            set;
        }

        /// <summary>
        /// True to pause at the end of execution when an error occurs. Default
        /// is true.
        /// </summary>
        public bool WaitOnAbnormalExit {
            get;
            set;
        }

        /// <summary>
        /// True to pause at the end of execution when completing successfully.
        /// Default is true.
        /// </summary>
        public bool WaitOnNormalExit {
            get;
            set;
        }

        /// <summary>
        /// True to break on a SystemExit exception even when its exit code is
        /// zero. This applies only when the debugger would normally break on
        /// a SystemExit exception. Default is false.
        /// </summary>
        /// <remarks>New in 1.1</remarks>
        public bool BreakOnSystemExitZero {
            get;
            set;
        }

        /// <summary>
        /// True if the standard launcher should allow debugging of the standard
        /// library. Default is false.
        /// </summary>
        /// <remarks>New in 1.1</remarks>
        public bool DebugStdLib {
            get;
            set;
        }
    }
}
