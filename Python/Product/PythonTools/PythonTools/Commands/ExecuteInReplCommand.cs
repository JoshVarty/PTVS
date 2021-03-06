using System;
using System.Diagnostics;
using Microsoft.PythonTools.Intellisense;
using Microsoft.PythonTools.Interpreter;
using Microsoft.PythonTools.Project;
using Microsoft.PythonTools.Repl;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudioTools;
using Microsoft.PythonTools.InteractiveWindow.Shell;
using Microsoft.PythonTools.Infrastructure;
using Microsoft.PythonTools.InteractiveWindow;

namespace Microsoft.PythonTools.Commands {
    /// <summary>
    /// Provides the command for starting a file or the start item of a project in the REPL window.
    /// </summary>
    internal sealed class ExecuteInReplCommand : Command {
        private readonly IServiceProvider _serviceProvider;

        public ExecuteInReplCommand(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        internal static IVsInteractiveWindow/*!*/ EnsureReplWindow(IServiceProvider serviceProvider, VsProjectAnalyzer analyzer, PythonProjectNode project) {
            return EnsureReplWindow(serviceProvider, analyzer.InterpreterFactory.Configuration, project);
        }

        internal static IVsInteractiveWindow/*!*/ EnsureReplWindow(IServiceProvider serviceProvider, InterpreterConfiguration config, PythonProjectNode project) {
            var compModel = serviceProvider.GetComponentModel();
            var provider = compModel.GetService<InteractiveWindowProvider>();
            var vsProjectContext = compModel.GetService<VsProjectContextProvider>();

            var projectId = project != null ? PythonReplEvaluatorProvider.GetEvaluatorId(project) : null;
            var configId = config != null ? PythonReplEvaluatorProvider.GetEvaluatorId(config) : null;

            IVsInteractiveWindow window;

            // If we find an open window for the project, prefer that to a per-config one
            if (!string.IsNullOrEmpty(projectId)) {
                window = provider.Open(projectId);
                if (window != null) {
                    if (window.InteractiveWindow.GetPythonEvaluator()?.AssociatedProjectHasChanged == true) {
                        // We have an existing window, but it needs to be reset.
                        // Let's create a new one
                        window = provider.Create(projectId);
                        project.AddActionOnClose(window, w => InteractiveWindowProvider.CloseIfEvaluatorMatches(w, projectId));
                    }

                    return window;
                }
            }

            // If we find an open window for the configuration, return that
            if (!string.IsNullOrEmpty(configId)) {
                window = provider.Open(configId);
                if (window != null) {
                    return window;
                }
            }

            // No window found, so let's create one
            if (!string.IsNullOrEmpty(projectId)) {
                window = provider.Create(projectId);
                project.AddActionOnClose(window, w => InteractiveWindowProvider.CloseIfEvaluatorMatches(w, projectId));
            } else if (!string.IsNullOrEmpty(configId)) {
                window = provider.Create(configId);
            } else {
                var interpService = compModel.GetService<IInterpreterOptionsService>();
                window = provider.Create(PythonReplEvaluatorProvider.GetEvaluatorId(interpService.DefaultInterpreter.Configuration));
            }

            return window;
        }

        public override EventHandler BeforeQueryStatus {
            get {
                return QueryStatusMethod;
            }
        }

        private void QueryStatusMethod(object sender, EventArgs args) {
            var oleMenu = sender as OleMenuCommand;
            if (oleMenu == null) {
                Debug.Fail("Unexpected command type " + sender == null ? "(null)" : sender.GetType().FullName);
                return;
            }

            var pyProj = CommonPackage.GetStartupProject(_serviceProvider) as PythonProjectNode;
            var textView = CommonPackage.GetActiveTextView(_serviceProvider);

            oleMenu.Supported = true;

            if (pyProj != null) {
                // startup project, so visible in Project mode
                oleMenu.Visible = true;
                oleMenu.Text = Strings.ExecuteInReplCommand_ExecuteProject;

                // Only enable if runnable
                oleMenu.Enabled = pyProj.GetInterpreterFactory().IsRunnable();

            } else if (textView != null && textView.TextBuffer.ContentType.IsOfType(PythonCoreConstants.ContentType)) {
                // active file, so visible in File mode
                oleMenu.Visible = true;
                oleMenu.Text = Strings.ExecuteInReplCommand_ExecuteFile;

                // Only enable if runnable
                var interpreterService = _serviceProvider.GetComponentModel().GetService<IInterpreterOptionsService>();
                oleMenu.Enabled = interpreterService != null && interpreterService.DefaultInterpreter.IsRunnable();

            } else {
                // Python is not active, so hide the command
                oleMenu.Visible = false;
                oleMenu.Enabled = false;
            }
        }

        public override async void DoCommand(object sender, EventArgs e) {
            var pyProj = CommonPackage.GetStartupProject(_serviceProvider) as PythonProjectNode;
            var textView = CommonPackage.GetActiveTextView(_serviceProvider);

            var config = pyProj?.GetLaunchConfigurationOrThrow();
            if (config == null && textView != null) {
                var interpreters = _serviceProvider.GetComponentModel().GetService<IInterpreterOptionsService>();
                config = new LaunchConfiguration(interpreters.DefaultInterpreter.Configuration) {
                    ScriptName = textView.GetFilePath(),
                    WorkingDirectory = PathUtils.GetParent(textView.GetFilePath())
                };
            }
            if (config == null) {
                Debug.Fail("Should not be executing command when it is invisible");
                return;
            }

            var window = EnsureReplWindow(_serviceProvider, config.Interpreter, pyProj);
            window.Show(true);

            var eval = (IPythonInteractiveEvaluator)window.InteractiveWindow.Evaluator;

            // The interpreter may take some time to startup, do this off the UI thread.
            await ThreadHelper.JoinableTaskFactory.RunAsync(async () => {
                await ((IInteractiveEvaluator)eval).ResetAsync();

                window.InteractiveWindow.WriteLine(Strings.ExecuteInReplCommand_RunningMessage.FormatUI(config.ScriptName));

                await eval.ExecuteFileAsync(config.ScriptName, config.ScriptArguments);
            });
        }

        public override int CommandId {
            get { return (int)PkgCmdIDList.cmdidExecuteFileInRepl; }
        }
    }
}
