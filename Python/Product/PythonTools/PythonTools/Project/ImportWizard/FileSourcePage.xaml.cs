using System.Diagnostics;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.PythonTools.Infrastructure;

namespace Microsoft.PythonTools.Project.ImportWizard {
    /// <summary>
    /// Interaction logic for FileSourcePage.xaml
    /// </summary>
    internal partial class FileSourcePage : Page {
        public FileSourcePage() {
            InitializeComponent();
        }

        private async void SourcePathTextBox_SourceUpdated(object sender, DataTransferEventArgs e) {
            Debug.Assert(DataContext is ImportSettings);
            var settings = (ImportSettings)DataContext;
            SourcePathDoesNotExist.Visibility = 
                (string.IsNullOrEmpty(settings.SourcePath) || Directory.Exists(settings.SourcePath)) ?
                System.Windows.Visibility.Collapsed :
                System.Windows.Visibility.Visible;
            await settings.UpdateSourcePathAsync().HandleAllExceptions(null);
        }
    }
}
