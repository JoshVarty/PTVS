using System;
using System.IO;
using System.Windows;
using Microsoft.PythonTools.Infrastructure;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudioTools;

namespace Microsoft.PythonTools.Commands {
    /// <summary>
    /// Provides the command to import a project from existing code.
    /// </summary>
    class ImportWizardCommand : Command {
        private readonly IServiceProvider _serviceProvider;

        public ImportWizardCommand(IServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        private async void CreateProjectAndHandleErrors(
            IVsStatusbar statusBar,
            Microsoft.PythonTools.Project.ImportWizard.ImportWizard dlg
        ) {
            try {
                var path = await dlg.ImportSettings.CreateRequestedProjectAsync();
                if (File.Exists(path)) {
                    object outRef = null, pathRef = ProcessOutput.QuoteSingleArgument(path);
                    _serviceProvider.GetDTE().Commands.Raise(
                        VSConstants.GUID_VSStandardCommandSet97.ToString("B"),
                        (int)VSConstants.VSStd97CmdID.OpenProject,
                        ref pathRef,
                        ref outRef
                    );
                    statusBar.SetText("");
                    return;
                }
            } catch (UnauthorizedAccessException) {
                MessageBox.Show(Strings.ErrorImportWizardUnauthorizedAccess, Strings.ProductTitle);
            } catch (Exception ex) {
                ActivityLog.LogError(Strings.ProductTitle, ex.ToString());
                MessageBox.Show(Strings.ErrorImportWizardException.FormatUI(ex.GetType().Name), Strings.ProductTitle);
            }
            statusBar.SetText(Strings.StatusImportWizardError);
        }

        public override void DoCommand(object sender, EventArgs args) {
            var statusBar = (IVsStatusbar)_serviceProvider.GetService(typeof(SVsStatusbar));
            statusBar.SetText(Strings.StatusImportWizardStarting);

            string initialProjectPath = null, initialSourcePath = null;

            var oleArgs = args as Microsoft.VisualStudio.Shell.OleMenuCmdEventArgs;
            if (oleArgs != null) {
                string projectArgs = oleArgs.InValue as string;
                if (projectArgs != null) {
                    var argItems = projectArgs.Split('|');
                    if (argItems.Length == 2) {
                        initialProjectPath = PathUtils.GetAvailableFilename(
                            argItems[1],
                            argItems[0],
                            ".pyproj"
                        );
                        initialSourcePath = argItems[1];
                    }
                }
            }

            var dlg = new Microsoft.PythonTools.Project.ImportWizard.ImportWizard(
                _serviceProvider,
                initialSourcePath,
                initialProjectPath
            );

            if (dlg.ShowModal() ?? false) {
                CreateProjectAndHandleErrors(statusBar, dlg);
            } else {
                statusBar.SetText("");
            }
        }

        public override int CommandId {
            get { return (int)PkgCmdIDList.cmdidImportWizard; }
        }
    }
}
