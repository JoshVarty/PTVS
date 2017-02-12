using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.PythonTools;
using Microsoft.PythonTools.Project;
using Microsoft.VisualStudioTools.Project;

namespace Microsoft.PythonTools.Django.Project {
    public partial class DjangoPropertyPageControl : UserControl {
        private readonly DjangoPropertyPage _properties;

        private DjangoPropertyPageControl() {
            InitializeComponent();

            _toolTip.SetToolTip(_settingsModule, Resources.DjangoSettingsModuleHelp);
            _toolTip.SetToolTip(_settingsModuleLabel, Resources.DjangoSettingsModuleHelp);

            _toolTip.SetToolTip(_staticUri, Resources.StaticUriHelp);
            _toolTip.SetToolTip(_staticUriLabel, Resources.StaticUriHelp);
        }

        internal DjangoPropertyPageControl(DjangoPropertyPage properties)
            : this() {
            _properties = properties;
        }

        public string SettingsModule {
            get { return _settingsModule.Text; }
            set { _settingsModule.Text = value; }
        }

        public string StaticUriPattern {
            get { return _staticUri.Text; }
            set { _staticUri.Text = value; }
        }

        private void Setting_TextChanged(object sender, EventArgs e) {
            if (_properties != null) {
                _properties.IsDirty = true;
            }
        }
    }
}
