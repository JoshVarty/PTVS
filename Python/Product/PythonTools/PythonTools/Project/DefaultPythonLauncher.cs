using System;
using System.Diagnostics;
using Microsoft.PythonTools.Debugger;
using Microsoft.PythonTools.Infrastructure;
using Microsoft.PythonTools.Interpreter;
using Microsoft.VisualStudio;
using Microsoft.VisualStudioTools.Project;

namespace Microsoft.PythonTools.Project {
    /// <summary>
    /// Implements functionality of starting a project or a file with or without debugging.
    /// </summary>
    sealed class DefaultPythonLauncher : IProjectLauncher {
        private readonly IServiceProvider _serviceProvider;
        private readonly LaunchConfiguration _config;

        public DefaultPythonLauncher(IServiceProvider serviceProvider, LaunchConfiguration config) {
            _serviceProvider = serviceProvider;
            _config = config;
        }

        public int LaunchProject(bool debug) {
            return Launch(_config, debug);
        }

        public int LaunchFile(string/*!*/ file, bool debug) {
            var config = _config.Clone();
            config.ScriptName = file;
            return Launch(config, debug);
        }

        private int Launch(LaunchConfiguration config, bool debug) {
            if (debug) {
                StartWithDebugger(config);
            } else {
                StartWithoutDebugger(config).Dispose();
            }

            return VSConstants.S_OK;
        }

        /// <summary>
        /// Default implementation of the "Start without Debugging" command.
        /// </summary>
        private Process StartWithoutDebugger(LaunchConfiguration config) {
            try {
                _serviceProvider.GetPythonToolsService().Logger.LogEvent(Logging.PythonLogEvent.Launch, new Logging.LaunchInfo {
                    IsDebug = false,
                    Version = config.Interpreter?.Version.ToString() ?? ""
                });
            } catch (Exception ex) {
                Debug.Fail(ex.ToUnhandledExceptionMessage(GetType()));
            }
            return Process.Start(DebugLaunchHelper.CreateProcessStartInfo(_serviceProvider, config));
        }

        /// <summary>
        /// Default implementation of the "Start Debugging" command.
        /// </summary>
        private void StartWithDebugger(LaunchConfiguration config) {
            try {
                _serviceProvider.GetPythonToolsService().Logger.LogEvent(Logging.PythonLogEvent.Launch, new Logging.LaunchInfo {
                    IsDebug = true,
                    Version = config.Interpreter?.Version.ToString() ?? ""
                });
            } catch (Exception ex) {
                Debug.Fail(ex.ToUnhandledExceptionMessage(GetType()));
            }

            // Historically, we would clear out config.InterpreterArguments at
            // this stage if doing mixed-mode debugging. However, there doesn't
            // seem to be any need to do this, so we now leave them alone.

            using (var dbgInfo = DebugLaunchHelper.CreateDebugTargetInfo(_serviceProvider, config)) {
                dbgInfo.Launch();
            }
        }
    }
}
