using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Microsoft.PythonTools;
using Microsoft.PythonTools.Project;
using Microsoft.VisualStudioTools.Project;

namespace Microsoft.PythonTools.Project.Web {
    public partial class PythonWebPropertyPageControl : UserControl {
        private readonly PythonWebPropertyPage _properties;
        private readonly Timer _validateStaticPatternTimer;

        private PythonWebPropertyPageControl() {
            InitializeComponent();

            _validateStaticPatternTimer = new Timer();
            _validateStaticPatternTimer.Tick += ValidateStaticPattern;
            _validateStaticPatternTimer.Interval = 500;

            _toolTip.SetToolTip(_staticPattern, Strings.StaticPatternHelp);
            _toolTip.SetToolTip(_staticPatternLabel, Strings.StaticPatternHelp);

            _toolTip.SetToolTip(_staticRewrite, Strings.StaticRewriteHelp);
            _toolTip.SetToolTip(_staticRewriteLabel, Strings.StaticRewriteHelp);

            _toolTip.SetToolTip(_wsgiHandler, Strings.WsgiHandlerHelp);
            _toolTip.SetToolTip(_wsgiHandlerLabel, Strings.WsgiHandlerHelp);
        }

        private void ValidateStaticPattern(object sender, EventArgs e) {
            _validateStaticPatternTimer.Enabled = false;

            try {
                new Regex(_staticPattern.Text);
                _errorProvider.SetError(_staticPattern, null);
            } catch (ArgumentException) {
                _errorProvider.SetError(_staticPattern, Strings.StaticPatternError);
            }
        }

        internal PythonWebPropertyPageControl(PythonWebPropertyPage properties)
            : this() {
            _properties = properties;
        }

        public string StaticUriPattern {
            get { return _staticPattern.Text; }
            set { _staticPattern.Text = value; }
        }

        public string StaticUriRewrite {
            get { return _staticRewrite.Text; }
            set { _staticRewrite.Text = value; }
        }

        public string WsgiHandler {
            get { return _wsgiHandler.Text; }
            set { _wsgiHandler.Text = value; }
        }

        private void Setting_TextChanged(object sender, EventArgs e) {
            if (_properties != null) {
                _properties.IsDirty = true;
            }
            if (sender == _staticPattern) {
                _validateStaticPatternTimer.Enabled = false;
                _validateStaticPatternTimer.Enabled = true;
            }
        }
    }
}
