using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.VisualStudioTools.Project;

namespace Microsoft.PythonTools.Django.Project {
    [Guid(GuidList.guidDjangoPropertyPageString)]
    class DjangoPropertyPage : CommonPropertyPage {
        private readonly DjangoPropertyPageControl _control;

        public const string SettingModulesSetting = "DjangoSettingsModule";
        public const string StaticUriPatternSetting = "StaticUriPattern";

        public DjangoPropertyPage() {
            _control = new DjangoPropertyPageControl(this);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                _control.Dispose();
            }
            base.Dispose(disposing);
        }

        public override Control Control {
            get { return _control; }
        }

        public override void Apply() {
            SetProjectProperty(SettingModulesSetting, _control.SettingsModule);
            SetProjectProperty(StaticUriPatternSetting, _control.StaticUriPattern);
            IsDirty = false;
        }

        public override void LoadSettings() {
            Loading = true;
            try {
                _control.SettingsModule = GetProjectProperty(SettingModulesSetting);
                _control.StaticUriPattern = GetProjectProperty(StaticUriPatternSetting);
                IsDirty = false;
            } finally {
                Loading = false;
            }
        }

        public override string Name {
            get { return Resources.DjangoPropertyPageTitle; }
        }
    }
}
