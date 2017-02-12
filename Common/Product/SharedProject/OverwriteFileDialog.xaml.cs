using System.Windows;

namespace Microsoft.VisualStudioTools {
    /// <summary>
    /// Interaction logic for OverwriteFileDialog.xaml
    /// </summary>
    internal partial class OverwriteFileDialog : DialogWindowVersioningWorkaround {
        public bool ShouldOverwrite;

        public OverwriteFileDialog() {
            InitializeComponent();
        }

        public OverwriteFileDialog(string message, bool doForAllItems) {
            InitializeComponent();

            if (!doForAllItems) {
                _allItems.Visibility = Visibility.Hidden;
            }

            _message.Text = message;
        }

        
        private void YesClick(object sender, RoutedEventArgs e) {
            ShouldOverwrite = true;
            DialogResult = true;
            Close();
        }

        private void NoClick(object sender, RoutedEventArgs e) {
            ShouldOverwrite = false;
            DialogResult = true;
            Close();
        }

        private void CancelClick(object sender, RoutedEventArgs e) {
            Close();
        }

        public bool AllItems {
            get {
                return _allItems.IsChecked.Value;
            }
        }
    }
}
