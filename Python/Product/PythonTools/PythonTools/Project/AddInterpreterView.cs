using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Microsoft.PythonTools.Infrastructure;

namespace Microsoft.PythonTools.Project {
    sealed class AddInterpreterView : DependencyObject, IDisposable {
        private readonly PythonProjectNode _project;

        public AddInterpreterView(
            PythonProjectNode project,
            IServiceProvider serviceProvider,
            IEnumerable<string> selectedIds
        ) {
            _project = project;
            Interpreters = new ObservableCollection<InterpreterView>(InterpreterView.GetInterpreters(serviceProvider, project));
            
            var map = new Dictionary<string, InterpreterView>();
            foreach (var view in Interpreters) {
                map[view.Id] = view;
                view.IsSelected = false;
            }

            foreach (var id in selectedIds) {
                InterpreterView view;
                if (map.TryGetValue(id, out view)) {
                    view.IsSelected = true;
                }
            }

            _project.InterpreterFactoriesChanged += OnInterpretersChanged;
        }

        public void Dispose() {
            _project.InterpreterFactoriesChanged -= OnInterpretersChanged;
        }

        private void OnInterpretersChanged(object sender, EventArgs e) {
            if (!Dispatcher.CheckAccess()) {
                Dispatcher.BeginInvoke((Action)(() => OnInterpretersChanged(sender, e)));
                return;
            }
            var def = _project.ActiveInterpreter;
            Interpreters.Merge(
                InterpreterView.GetInterpreters(_project.Site, _project),
                InterpreterView.EqualityComparer,
                InterpreterView.Comparer
            );
        }

        public ObservableCollection<InterpreterView> Interpreters { get; }
    }
}
