using System;
using System.IO;
using System.Windows;

namespace Microsoft.PythonTools.Profiling {
    /// <summary>
    /// Interaction logic for LaunchProfiling.xaml
    /// </summary>
    public partial class LaunchProfiling : DialogWindowVersioningWorkaround {
        readonly ProfilingTargetView _viewModel;
        private readonly IServiceProvider _serviceProvider;

        public LaunchProfiling(IServiceProvider serviceProvider, ProfilingTargetView viewModel) {
            _serviceProvider = serviceProvider;
            _viewModel = viewModel;
            DataContext = _viewModel;
            InitializeComponent();
        }

        private void FindInterpreterClick(object sender, RoutedEventArgs e) {
            var standalone = _viewModel.Standalone;
            if (standalone != null) {
                var path = _serviceProvider.BrowseForFileOpen(
                    new System.Windows.Interop.WindowInteropHelper(this).Handle,
                    Strings.ExecutableFilesFilter,
                    standalone.InterpreterPath
                );
                if (File.Exists(path)) {
                    standalone.InterpreterPath = path;
                }
            }
        }

        private void FindScriptClick(object sender, RoutedEventArgs e) {
            var standalone = _viewModel.Standalone;
            if (standalone != null) {
                var path = _serviceProvider.BrowseForFileOpen(
                    new System.Windows.Interop.WindowInteropHelper(this).Handle,
                    Strings.PythonFilesFilter,
                    standalone.ScriptPath
                );
                if (File.Exists(path)) {
                    standalone.ScriptPath = path;
                }
            }
        }

        private void FindWorkingDirectoryClick(object sender, RoutedEventArgs e) {
            var standalone = _viewModel.Standalone;
            if (standalone != null) {
                var path = _serviceProvider.BrowseForDirectory(
                    new System.Windows.Interop.WindowInteropHelper(this).Handle,
                    standalone.WorkingDirectory
                );
                if (!string.IsNullOrEmpty(path)) {
                    standalone.WorkingDirectory = path;
                }
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
