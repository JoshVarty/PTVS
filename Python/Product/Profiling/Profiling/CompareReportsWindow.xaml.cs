using System;
using System.Windows;
using System.Windows.Input;
using Microsoft.VisualStudio.PlatformUI;
using Microsoft.Win32;

namespace Microsoft.PythonTools.Profiling {
    /// <summary>
    /// Interaction logic for CompareReportsWindow.xaml
    /// </summary>
    public partial class CompareReportsWindow : DialogWindowVersioningWorkaround {
        private readonly CompareReportsView _viewModel;
        
        public CompareReportsWindow(CompareReportsView viewModel) {
            _viewModel = viewModel;

            InitializeComponent();

            DataContext = viewModel;
        }

        private void OkClick(object sender, RoutedEventArgs e) {
            DialogResult = true;
            Close();
        }

        private void CancelClick(object sender, RoutedEventArgs e) {
            DialogResult = false;
            Close();
        }

        private string OpenFileDialog() {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = _viewModel.PerformanceFileFilter;
            dialog.CheckFileExists = true;
            bool res = dialog.ShowDialog() ?? false;
            if (res) {
                return dialog.FileName;
            }
            return null;
        }

        private void BaselineBrowseClick(object sender, RoutedEventArgs e) {
            var newFile = OpenFileDialog();
            if (!string.IsNullOrEmpty(newFile)) {
                _viewModel.BaselineFile = newFile;
            }
        }

        private void CompareBrowseClick(object sender, RoutedEventArgs e) {
            var newFile = OpenFileDialog();
            if (!string.IsNullOrEmpty(newFile)) {
                _viewModel.ComparisonFile = newFile;
            }
        }

        private void Browse_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            Microsoft.VisualStudioTools.Wpf.Commands.CanExecute(this, sender, e);
        }

        private void Browse_Executed(object sender, ExecutedRoutedEventArgs e) {
            Microsoft.VisualStudioTools.Wpf.Commands.Executed(this, sender, e);
        }
    }
}
