using System;
using System.Windows.Forms;

namespace Microsoft.PythonTools.Uwp.Project {
    public partial class PythonUwpPropertyPageControl : UserControl {
        private readonly PythonUwpPropertyPage _properties;

        private PythonUwpPropertyPageControl() {
            InitializeComponent();

            _toolTip.SetToolTip(_remoteDevice, Resources.UwpRemoteDeviceHelp);
            _toolTip.SetToolTip(_remoteDeviceLabel, Resources.UwpRemoteDeviceHelp);
            _toolTip.SetToolTip(_remotePort, Resources.UwpRemotePortHelp);
            _toolTip.SetToolTip(_remotePortLabel, Resources.UwpRemotePortHelp);
        }

        internal PythonUwpPropertyPageControl(PythonUwpPropertyPage properties)
            : this() {
            _properties = properties;
        }

        public string RemoteDevice {
            get { return _remoteDevice.Text; }
            set { _remoteDevice.Text = value; }
        }

        public decimal RemotePort {
            get { return _remotePort.Value; }
            set { _remotePort.Value = value; }
        }

        private void Setting_TextChanged(object sender, EventArgs e) {
            if (_properties != null) {
                _properties.IsDirty = true;
            }
        }
    }
}
