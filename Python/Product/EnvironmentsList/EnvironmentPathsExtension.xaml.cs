using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Microsoft.PythonTools.EnvironmentsList {
    sealed class EnvironmentPathsExtensionProvider : IEnvironmentViewExtension {
        private EnvironmentPathsExtension _wpfObject;

        public int SortPriority {
            get { return -10; }
        }

        public string LocalizedDisplayName {
            get { return Resources.EnvironmentPathsExtensionDisplayName; }
        }

        public object HelpContent {
            get { return Resources.EnvironmentPathsExtensionHelpContent; }
        }

        public FrameworkElement WpfObject {
            get {
                if (_wpfObject == null) {
                    _wpfObject = new EnvironmentPathsExtension();
                }
                return _wpfObject;
            }
        }
    }

    public partial class EnvironmentPathsExtension : UserControl {
        public static readonly ICommand OpenInBrowser = new RoutedCommand();
        public static readonly ICommand OpenInFileExplorer = new RoutedCommand();
        public static readonly ICommand StartInterpreter = new RoutedCommand();
        public static readonly ICommand StartWindowsInterpreter = new RoutedCommand();
        public static readonly ICommand ConfigureEnvironment = new RoutedCommand();

        public EnvironmentPathsExtension() {
            InitializeComponent();
        }

        void OpenInFileExplorer_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            var path = e.Parameter as string;
            e.CanExecute = File.Exists(path) || Directory.Exists(path);
        }

        private void OpenInFileExplorer_Executed(object sender, ExecutedRoutedEventArgs e) {
            var path = (string)e.Parameter;
            var psi = new ProcessStartInfo();
            psi.UseShellExecute = false;
            psi.FileName = "explorer.exe";

            if (File.Exists(path)) {
                psi.Arguments = "/select,\"" + path + "\"";
            } else if (Directory.Exists(path)) {
                psi.Arguments = "\"" + path + "\"";
            }

            Process.Start(psi);
        }

        private void CopyToClipboard_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = e.Parameter is string || e.Parameter is IDataObject;
            e.Handled = true;
        }

        private void CopyToClipboard_Executed(object sender, ExecutedRoutedEventArgs e) {
            var str = e.Parameter as string;
            if (str != null) {
                Clipboard.SetText(str);
            } else {
                Clipboard.SetDataObject((IDataObject)e.Parameter);
            }
        }
    }
}
