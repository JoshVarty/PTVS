using System.Windows;
using Microsoft.VisualStudioTools;

namespace Microsoft.PythonTools.Refactoring {
    /// <summary>
    /// Interaction logic for ExtractMethodDialog.xaml
    /// </summary>
    internal partial class ExtractMethodDialog : DialogWindowVersioningWorkaround {
        private bool _firstActivation;

        public ExtractMethodDialog(ExtractMethodRequestView viewModel) {
            DataContext = viewModel;

            InitializeComponent();

            _firstActivation = true;
        }

        protected override void OnActivated(System.EventArgs e) {
            base.OnActivated(e);
            if (_firstActivation) {
                _methodName.Focus();
                _methodName.SelectAll();
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
