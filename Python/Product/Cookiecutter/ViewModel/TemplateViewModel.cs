using System.ComponentModel;
using Microsoft.CookiecutterTools.Infrastructure;
using Microsoft.CookiecutterTools.Model;

namespace Microsoft.CookiecutterTools.ViewModel {
    class TemplateViewModel : INotifyPropertyChanged {
        private string _displayName;
        private string _remoteUrl;
        private string _ownerUrl;
        private string _ownerTooltip;
        private string _clonedPath;
        private string _description;
        private string _avatarUrl;
        private bool _isSearchTerm;
        private bool _isUpdateAvailable;

        public event PropertyChangedEventHandler PropertyChanged;

        public TemplateViewModel() {
        }

        public override string ToString() => DisplayName;

        public bool Selectable => true;

        public string GitHubHomeUrl => RemoteUrl;

        public string GitHubIssuesUrl => RemoteUrl != null ? RemoteUrl + "/issues" : null;

        public string GitHubWikiUrl => RemoteUrl != null ? RemoteUrl + "/wiki" : null;

        public bool HasDetails {
            get {
                return !string.IsNullOrEmpty(Description) &&
                       !string.IsNullOrEmpty(AvatarUrl) &&
                       !string.IsNullOrEmpty(OwnerUrl);
            }
        }

        /// <summary>
        /// Repository name.
        /// </summary>
        public string RepositoryName {
            get {
                if (string.IsNullOrEmpty(RemoteUrl)) {
                    return string.Empty;
                }

                string owner;
                string name;
                ParseUtils.ParseGitHubRepoOwnerAndName(RemoteUrl, out owner, out name);
                return name;
            }
        }

        /// <summary>
        /// Repository owner.
        /// </summary>
        public string RepositoryOwner {
            get {
                if (string.IsNullOrEmpty(RemoteUrl)) {
                    return string.Empty;
                }

                string owner;
                string name;
                ParseUtils.ParseGitHubRepoOwnerAndName(RemoteUrl, out owner, out name);
                return owner;
            }
        }

        /// <summary>
        /// Repository full name, ie. 'owner/name'.
        /// </summary>
        public string RepositoryFullName {
            get {
                if (string.IsNullOrEmpty(RemoteUrl)) {
                    return string.Empty;
                }

                string owner;
                string name;
                ParseUtils.ParseGitHubRepoOwnerAndName(RemoteUrl, out owner, out name);
                if (!string.IsNullOrEmpty(owner) && !string.IsNullOrEmpty(name)) {
                    return owner + '/' + name;
                }
                return string.Empty;
            }
        }

        public string DisplayName {
            get {
                return _displayName;
            }

            set {
                if (value != _displayName) {
                    _displayName = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DisplayName)));
                }
            }
        }

        public string RemoteUrl {
            get {
                return _remoteUrl;
            }

            set {
                if (value != _remoteUrl) {
                    _remoteUrl = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RemoteUrl)));
                }

                RefreshOwnerTooltip();
            }
        }

        public string ClonedPath {
            get {
                return _clonedPath;
            }

            set {
                if (value != _clonedPath) {
                    _clonedPath = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ClonedPath)));
                }
            }
        }

        public string Description {
            get {
                return _description;
            }

            set {
                if (value != _description) {
                    _description = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Description)));
                }
            }
        }

        public string AvatarUrl {
            get {
                return _avatarUrl;
            }

            set {
                if (value != _avatarUrl) {
                    _avatarUrl = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AvatarUrl)));
                }
            }
        }

        public string OwnerUrl {
            get {
                return _ownerUrl;
            }

            set {
                if (value != _ownerUrl) {
                    _ownerUrl = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OwnerUrl)));
                }
            }
        }

        public string OwnerTooltip {
            get {
                return _ownerTooltip;
            }

            set {
                if (value != _ownerTooltip) {
                    _ownerTooltip = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OwnerTooltip)));
                }
            }
        }

        public bool IsSearchTerm {
            get {
                return _isSearchTerm;
            }

            set {
                if (value != _isSearchTerm) {
                    _isSearchTerm = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSearchTerm)));
                }
            }
        }

        public bool IsUpdateAvailable {
            get {
                return _isUpdateAvailable;
            }

            set {
                if (value != _isUpdateAvailable) {
                    _isUpdateAvailable = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsUpdateAvailable)));
                }
            }
        }

        private void RefreshOwnerTooltip() {
            var owner = RepositoryOwner;
            if (string.IsNullOrEmpty(owner)) {
                owner = Strings.SearchPage_Creator;
            }

            OwnerTooltip = Strings.SearchPage_VisitOwnerOnGitHub.FormatUI(owner);
        }
    }
}
