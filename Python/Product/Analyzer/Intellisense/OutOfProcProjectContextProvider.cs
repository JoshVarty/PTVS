using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.PythonTools.Interpreter;

namespace Microsoft.PythonTools.Intellisense {
    [Export(typeof(IProjectContextProvider))]
    [Export(typeof(OutOfProcProjectContextProvider))]
    sealed class OutOfProcProjectContextProvider : IProjectContextProvider {
        private readonly HashSet<object> _contexts = new HashSet<object>();

        public void AddContext(object context) {
            bool added = false;
            lock (_contexts) {
                added = _contexts.Add(context);
            }
            if (added) {
                ProjectsChanaged?.Invoke(this, EventArgs.Empty);
            }
        }

        internal void RemoveContext(object context) {
            bool removed = false;
            lock (_contexts) {
                removed = _contexts.Remove(context);
            }
            if (removed) {
                ProjectsChanaged?.Invoke(this, EventArgs.Empty);
            }
        }

        public void InterpreterLoaded(object context, InterpreterConfiguration factory) {
        }

        public void InterpreterUnloaded(object context, InterpreterConfiguration factory) {
        }

        public IEnumerable<object> Projects {
            get {
                return _contexts.ToArray();
            }
        }

        public event EventHandler ProjectsChanaged;
        public event EventHandler<ProjectChangedEventArgs> ProjectChanged {
            add {
                
            }
            remove {
            }
        }
    }
}
