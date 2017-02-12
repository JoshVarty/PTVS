using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Microsoft.PythonTools.Interpreter;
using Microsoft.VisualStudioTools;

namespace Microsoft.PythonTools.Project {
    /// <summary>
    /// Interaction logic for AddInterpreter.xaml
    /// </summary>
    sealed partial class AddInterpreter : DialogWindowVersioningWorkaround, IDisposable {
        private readonly AddInterpreterView _view;

        private AddInterpreter(PythonProjectNode project, IInterpreterOptionsService service) {
            _view = new AddInterpreterView(project, project.Site, project.InterpreterIds);
            DataContext = _view;

            InitializeComponent();
        }

        public void Dispose() {
            _view.Dispose();
        }

        public static IEnumerable<string> ShowDialog(
            PythonProjectNode project,
            IInterpreterOptionsService service) {
            using (var wnd = new AddInterpreter(project, service)) {
                if (wnd.ShowModal() ?? false) {
                    return wnd._view.Interpreters.Where(iv => iv.IsSelected).Select(iv => iv.Id).ToArray();
                }
            }
            return null;
        }

        private void Close_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void Close_Executed(object sender, ExecutedRoutedEventArgs e) {
            DialogResult = false;
            Close();
        }

        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e) {
            DialogResult = true;
            Close();
        }

        private void WebChooseInterpreter_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = true;
        }

        private void WebChooseInterpreter_Executed(object sender, ExecutedRoutedEventArgs e) {
            PythonToolsPackage.OpenWebBrowser(PythonToolsPackage.InterpreterHelpUrl);
        }
    }
}
