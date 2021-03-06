using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.VisualStudioTools;
using Microsoft.VisualStudioTools.Project;

namespace Microsoft.PythonTools.Project {
    [SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable",
        Justification = "object is owned by VS")]
    [Guid(PythonConstants.GeneralPropertyPageGuid)]
    public class PythonGeneralPropertyPage : CommonPropertyPage {
        private readonly PythonGeneralPropertyPageControl _control;

        public PythonGeneralPropertyPage() {
            _control = new PythonGeneralPropertyPageControl(this);
        }

        public override Control Control {
            get {
                return _control;
            }
        }

        internal override CommonProjectNode Project {
            get {
                return base.Project;
            }
            set {
                if (base.Project != null) {
                    PythonProject.InterpreterFactoriesChanged -= OnInterpretersChanged;
                    PythonProject.ActiveInterpreterChanged -= OnInterpretersChanged;
                    base.Project.PropertyPage = null;
                }
                base.Project = value;
                if (value != null) {
                    PythonProject.InterpreterFactoriesChanged += OnInterpretersChanged;
                    PythonProject.ActiveInterpreterChanged += OnInterpretersChanged;
                    value.PropertyPage = this;
                }
            }
        }

        private void OnInterpretersChanged(object sender, EventArgs e) {
            if (_control.InvokeRequired) {
                _control.BeginInvoke((Action)_control.OnInterpretersChanged);
            } else {
                _control.OnInterpretersChanged();
            }
        }

        internal PythonProjectNode PythonProject {
            get {
                return (PythonProjectNode)Project;
            }
        }

        public override string Name {
            get { return Strings.PythonGeneralPropertyPageLabel; }
        }

        public override void Apply() {
            Project.SetProjectProperty(CommonConstants.StartupFile, _control.StartupFile);
            Project.SetProjectProperty(CommonConstants.WorkingDirectory, _control.WorkingDirectory);
            Project.SetProjectProperty(CommonConstants.IsWindowsApplication, _control.IsWindowsApplication.ToString());
            
            var interp = _control.DefaultInterpreter;
            if (interp != null && !PythonProject.InterpreterFactories.Contains(interp)) {
                PythonProject.AddInterpreter(interp.Configuration.Id);
            }
            PythonProject.SetInterpreterFactory(_control.DefaultInterpreter);
            IsDirty = false;
            LoadSettings();
        }

        public override void LoadSettings() {
            Loading = true;
            try {
                _control.LoadSettings();
            } finally {
                Loading = false;
            }
        }
    }
}
