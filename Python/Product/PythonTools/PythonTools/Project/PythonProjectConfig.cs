using System;
using System.Windows.Forms;
using Microsoft.VisualStudio;
using Microsoft.VisualStudioTools.Project;

namespace Microsoft.PythonTools.Project {
    class PythonProjectConfig : CommonProjectConfig {
        private readonly PythonProjectNode _project;

        public PythonProjectConfig(PythonProjectNode project, string configuration)
            : base(project, configuration) {
            _project = project;
        }

        /// <summary>
        /// The display name is a two part item
        /// first part is the config name, 2nd part is the platform name
        /// </summary>
        public override int get_DisplayName(out string name) {
            if (!string.IsNullOrEmpty(PlatformName)) {
                name = ConfigName + "|" + PlatformName;
                return VSConstants.S_OK;
            } else {
                return base.get_DisplayName(out name);
            }
        }

        public override int DebugLaunch(uint flags) {
            if (_project.ShouldWarnOnLaunch) {
                var pyService = ProjectMgr.Site.GetPythonToolsService();
                if (pyService.DebuggerOptions.PromptBeforeRunningWithBuildError) {
                    var res = new StartWithErrorsDialog(pyService).ShowDialog();
                    if (res == DialogResult.No) {
                        return VSConstants.S_OK;
                    }
                }
            }

            try {
                return base.DebugLaunch(flags);
            } catch (MissingInterpreterException ex) {
                if (_project.ActiveInterpreter == _project.InterpreterRegistry.NoInterpretersValue) {
                    PythonToolsPackage.OpenNoInterpretersHelpPage(ProjectMgr.Site, ex.HelpPage);
                } else {
                    MessageBox.Show(ex.Message, Strings.ProductTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                return VSConstants.S_OK;
            } catch (NoInterpretersException ex) {
                PythonToolsPackage.OpenNoInterpretersHelpPage(ProjectMgr.Site, ex.HelpPage);
                return VSConstants.S_OK;
            }
        }
    }
}
