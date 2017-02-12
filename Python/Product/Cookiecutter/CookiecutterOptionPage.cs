using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace Microsoft.CookiecutterTools {
    [Guid("BDB4E0B1-4869-4A6F-AD55-5230B768261D")]
    public class CookiecutterOptionPage : DialogPage {
        private bool _showHelp = true;
        private bool _checkForTemplateUpdate = true;
        private string _feedUrl = UrlConstants.DefaultRecommendedFeed;

        public CookiecutterOptionPage() {
        }

        [SRCategory(SR.SettingsGeneralCategory)]
        [SRDisplayName(SR.SettingsShowHelpName)]
        [SRDescription(SR.SettingsShowHelpDescription)]
        [DefaultValue(true)]
        public bool ShowHelp {
            get { return _showHelp; }
            set { _showHelp = value; }
        }

        [SRCategory(SR.SettingsGeneralCategory)]
        [SRDisplayName(SR.SettingsFeedUrlName)]
        [SRDescription(SR.SettingsFeedUrlDescription)]
        [DefaultValue(UrlConstants.DefaultRecommendedFeed)]
        public string FeedUrl {
            get { return _feedUrl; }
            set { _feedUrl = value; }
        }

        [SRCategory(SR.SettingsGeneralCategory)]
        [SRDisplayName(SR.SettingsCheckForTemplateUpdateName)]
        [SRDescription(SR.SettingsCheckForTemplateUpdateDescription)]
        [DefaultValue(true)]
        public bool CheckForTemplateUpdate {
            get { return _checkForTemplateUpdate; }
            set { _checkForTemplateUpdate = value; }
        }
    }
}
