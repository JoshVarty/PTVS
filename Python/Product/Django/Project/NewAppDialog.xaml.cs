using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.Shell;

namespace Microsoft.PythonTools.Django.Project {
    /// <summary>
    /// Interaction logic for NewAppDialog.xaml
    /// </summary>
    partial class NewAppDialog : DialogWindowVersioningWorkaround {
        private readonly NewAppDialogViewModel _viewModel;

        public static readonly object BackgroundKey = VsBrushes.WindowKey;
        public static readonly object ForegroundKey = VsBrushes.WindowTextKey;
        
        public NewAppDialog() {
            InitializeComponent();

            DataContext = _viewModel = new NewAppDialogViewModel();

            _newAppName.Focus();
        }

        internal NewAppDialogViewModel ViewModel {
            get {
                return _viewModel;
            }
        }

        private void _ok_Click(object sender, RoutedEventArgs e) {
            DialogResult = true;
            Close();
        }

        private void _cancel_Click(object sender, RoutedEventArgs e) {
            DialogResult = false;
            Close();
        }
    }
}
