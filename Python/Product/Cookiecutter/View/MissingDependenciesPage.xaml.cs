using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;

namespace Microsoft.CookiecutterTools.View {
    /// <summary>
    /// Interaction logic for MissingDependenciesPage.xaml
    /// </summary>
    internal partial class MissingDependenciesPage : Page {
        public static readonly ICommand InstallPython = new RoutedCommand();

        public MissingDependenciesPage() {
            InitializeComponent();
        }

        private void InstallPython_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            var url = (string)e.Parameter;
            e.CanExecute = true;
            e.Handled = true;
        }

        private void InstallPython_Executed(object sender, ExecutedRoutedEventArgs e) {
            var url = (string)e.Parameter;
            Process.Start(UrlConstants.InstallPythonUrl)?.Dispose();
        }
    }
}
