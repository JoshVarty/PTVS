using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.VisualStudioTools.Project;

namespace Microsoft.PythonTools.Project.Web {
    [SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable",
        Justification = "object is owned by VS")]
    [Guid(PythonConstants.WebPropertyPageGuid)]
    class PythonWebPropertyPage : CommonPropertyPage {
        private readonly PythonWebPropertyPageControl _control;

        public const string StaticUriPatternSetting = "StaticUriPattern";
        public const string StaticUriRewriteSetting = "StaticUriRewrite";
        public const string WsgiHandlerSetting = "PythonWsgiHandler";

        public PythonWebPropertyPage() {
            _control = new PythonWebPropertyPageControl(this);
        }

        public override Control Control {
            get { return _control; }
        }

        public override void Apply() {
            SetProjectProperty(StaticUriPatternSetting, _control.StaticUriPattern);
            SetProjectProperty(StaticUriRewriteSetting, _control.StaticUriRewrite);
            SetProjectProperty(WsgiHandlerSetting, _control.WsgiHandler);
            IsDirty = false;
        }

        public override void LoadSettings() {
            Loading = true;
            try {
                _control.StaticUriPattern = GetProjectProperty(StaticUriPatternSetting);
                _control.StaticUriRewrite = GetProjectProperty(StaticUriRewriteSetting);
                _control.WsgiHandler = GetProjectProperty(WsgiHandlerSetting);
                IsDirty = false;
            } finally {
                Loading = false;
            }
        }

        public override string Name {
            get { return Strings.PythonWebPropertyPageTitle; }
        }
    }
}
