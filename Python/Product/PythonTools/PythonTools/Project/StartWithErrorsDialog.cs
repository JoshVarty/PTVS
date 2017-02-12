using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System;

namespace Microsoft.PythonTools.Project {
    public partial class StartWithErrorsDialog : Form {
        private readonly PythonToolsService _pyService;

        public StartWithErrorsDialog(PythonToolsService pyService) {
            _pyService = pyService;
            InitializeComponent();
            _icon.Image = SystemIcons.Warning.ToBitmap();
        }

        [Obsolete("Use PythonToolsService.DebuggerOptions.PromptBeforeRunningWithBuildError instead")]
        public static bool ShouldShow {
            get {
                var pyService = (PythonToolsService)PythonToolsPackage.GetGlobalService(typeof(PythonToolsService));

                return pyService.DebuggerOptions.PromptBeforeRunningWithBuildError;
            }
        }

        protected override void OnClosing(CancelEventArgs e) {
            if (_dontShowAgainCheckbox.Checked) {
                _pyService.DebuggerOptions.PromptBeforeRunningWithBuildError = false;
                _pyService.DebuggerOptions.Save();
            }
        }

        private void YesButtonClick(object sender, System.EventArgs e) {
            DialogResult = System.Windows.Forms.DialogResult.Yes;
            Close();
        }

        private void NoButtonClick(object sender, System.EventArgs e) {
            this.DialogResult = System.Windows.Forms.DialogResult.No;
            Close();
        }

        internal PythonToolsService PythonService {
            get {
                return _pyService;
            }
        }
    }
}
