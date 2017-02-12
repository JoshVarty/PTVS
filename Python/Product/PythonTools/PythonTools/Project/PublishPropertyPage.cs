using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.VisualStudioTools;
using Microsoft.VisualStudioTools.Project;

namespace Microsoft.PythonTools.Project {
    [SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable",
        Justification = "object is owned by VS")]
    [Guid(PythonConstants.PublishPropertyPageGuid)]
    public sealed class PublishPropertyPage : CommonPropertyPage {
        private readonly PublishPropertyControl _control;

        public PublishPropertyPage() {
            _control = new PublishPropertyControl(this);
        }

        public override Control Control {
            get { return _control; }
        }

        public override void Apply() {
            Project.SetProjectProperty(CommonConstants.PublishUrl, _control.PublishUrl);
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

        public override string Name {
            get { return Strings.PythonPublishPropertyPageLabel; }
        }
    }
}
