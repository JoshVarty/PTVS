using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.VisualStudioTools.Project;

namespace Microsoft.PythonTools.Project {
    [SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable",
        Justification = "object is owned by VS")]
    [Guid(PythonConstants.DebugPropertyPageGuid)]
    class PythonDebugPropertyPage : CommonPropertyPage {
        private readonly PythonDebugPropertyPageControl _control;

        public PythonDebugPropertyPage() {
            _control = new PythonDebugPropertyPageControl(this);
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
                if (value == null && base.Project != null) {
                    ((PythonProjectNode)base.Project).DebugPropertyPage = null;
                }
                base.Project = value;
                if (value != null) {
                    ((PythonProjectNode)value).DebugPropertyPage = this;
                }
            }
        }

        public override string Name {
            get { return Strings.PythonDebugPropertyPageLabel; }
        }

        public override void Apply() {
            Project.SetProjectProperty(PythonConstants.LaunchProvider, _control.CurrentLauncher);
            _control.SaveSettings();

            IsDirty = false;
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
