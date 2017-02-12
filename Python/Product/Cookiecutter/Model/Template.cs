using System;

namespace Microsoft.CookiecutterTools.Model {
    class Template {
        public Template() {
            Name = string.Empty;
            RemoteUrl = string.Empty;
            LocalFolderPath = string.Empty;
            Description = string.Empty;
            AvatarUrl = string.Empty;
            OwnerUrl = string.Empty;
        }

        public string Name { get; set; }
        public string RemoteUrl { get; set; }
        public string LocalFolderPath { get; set; }
        public string Description { get; set; }
        public string AvatarUrl { get; set; }
        public string OwnerUrl { get; set; }
        public DateTime? ClonedLastUpdate { get; set; }
        public DateTime? RemoteLastUpdate { get; set; }
        public bool? UpdateAvailable {
            get {
                if (RemoteLastUpdate.HasValue && ClonedLastUpdate.HasValue) {
                    var span = RemoteLastUpdate - ClonedLastUpdate;
                    return span.Value.TotalMinutes > 2;
                }

                return null;
            }
        }

        public Template Clone() {
            return (Template)MemberwiseClone();
        }
    }
}
