using System.Windows;
using Microsoft.VisualStudioTools;

namespace Microsoft.PythonTools.Refactoring {
    /// <summary>
    /// Interaction logic for RenameVariableDialog.xaml
    /// </summary>
    partial class RenameVariableDialog : DialogWindowVersioningWorkaround {
        private bool _firstActivation;

        public RenameVariableDialog(RenameVariableRequestView viewModel) {
            DataContext = viewModel;

            InitializeComponent();

            _firstActivation = true;
        }

        protected override void OnActivated(System.EventArgs e) {
            base.OnActivated(e);
            if (_firstActivation) {
                _newName.Focus();
                _newName.SelectAll();
                _firstActivation = false;
            }
        }

        private void OkClick(object sender, RoutedEventArgs e) {
            this.DialogResult = true;
            Close();
        }

        private void CancelClick(object sender, RoutedEventArgs e) {
            this.DialogResult = false;
            Close();
        }
    }
}
