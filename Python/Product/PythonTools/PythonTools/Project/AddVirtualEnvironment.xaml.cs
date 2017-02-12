using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.PythonTools.Infrastructure;
using Microsoft.PythonTools.Interpreter;
using Microsoft.VisualStudioTools;
using WpfCommands = Microsoft.VisualStudioTools.Wpf.Commands;

namespace Microsoft.PythonTools.Project {
    /// <summary>
    /// Interaction logic for AddInterpreter.xaml
    /// </summary>
    partial class AddVirtualEnvironment : DialogWindowVersioningWorkaround {
        public static readonly ICommand WebChooseInterpreter = new RoutedCommand();

        private readonly IServiceProvider _site;
        private readonly AddVirtualEnvironmentView _view;
        private Task _currentOperation;

        private AddVirtualEnvironment(IServiceProvider site, AddVirtualEnvironmentView view) {
            _site = site;
            _view = view;
            _view.PropertyChanged += View_PropertyChanged;
            DataContext = _view;

            InitializeComponent();
        }

        public static async Task ShowDialog(
            PythonProjectNode project,
            IInterpreterRegistryService service,
            string requirementsPath,
            bool browseForExisting = false
        ) {
            using (var view = new AddVirtualEnvironmentView(project, service, project.ActiveInterpreter.Configuration.Id, requirementsPath)) {
                var wnd = new AddVirtualEnvironment(project.Site, view);

                if (browseForExisting) {
                    var path = project.Site.BrowseForDirectory(IntPtr.Zero, project.ProjectHome);
                    if (string.IsNullOrEmpty(path)) {
                        throw new OperationCanceledException();
                    }
                    view.VirtualEnvName = path;
                    view.WillInstallRequirementsTxt = false;
                    await view.WaitForReady();
                    if (view.WillAddVirtualEnv) {
                        await view.Create().HandleAllExceptions(project.Site, typeof(AddVirtualEnvironment));
                        return;
                    }

                    view.ShowBrowsePathError = true;
                    view.BrowseOrigPrefix = VirtualEnv.GetOrigPrefixPath(path);
                }

                wnd.VirtualEnvPathTextBox.ScrollToEnd();
                wnd.VirtualEnvPathTextBox.SelectAll();
                wnd.VirtualEnvPathTextBox.Focus();

                wnd.ShowModal();
                var op = wnd._currentOperation;
                if (op != null) {
                    await op;
                }
            }
        }

        private void View_PropertyChanged(object sender, PropertyChangedEventArgs e) {
            CommandManager.InvalidateRequerySuggested();
        }

        private void Browse_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            WpfCommands.CanExecute(this, sender, e);
        }

        private void Browse_Executed(object sender, ExecutedRoutedEventArgs e) {
            WpfCommands.Executed(this, sender, e);
        }

        private void Close_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void Close_Executed(object sender, ExecutedRoutedEventArgs e) {
            try {
                DialogResult = false;
                Close();
            } catch (InvalidOperationException) {
                // Dialog is already closed by the user
            }
        }

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = _currentOperation == null && 
                (_view.WillCreateVirtualEnv || _view.WillAddVirtualEnv);
        }

        private async void Save_Executed(object sender, ExecutedRoutedEventArgs e) {
            Debug.Assert(_currentOperation == null);
            _currentOperation = _view.Create().HandleAllExceptions(_site, GetType());
            
            await _currentOperation;

            _currentOperation = null;
            try {
                DialogResult = true;
                Close();
            } catch (InvalidOperationException) {
                // Dialog is already closed by the user
            }
        }

        private void WebChooseInterpreter_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void WebChooseInterpreter_Executed(object sender, ExecutedRoutedEventArgs e) {
            PythonToolsPackage.OpenWebBrowser(PythonToolsPackage.InterpreterHelpUrl);
            DialogResult = false;
            Close();
        }
    }
}
