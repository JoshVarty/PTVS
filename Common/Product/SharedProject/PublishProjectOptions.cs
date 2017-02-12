using System.Collections.Generic;

namespace Microsoft.VisualStudioTools.Project {
    public sealed class PublishProjectOptions {
        private readonly IPublishFile[] _additionalFiles;
        private readonly string _destination;
        public static readonly PublishProjectOptions Default = new PublishProjectOptions(new IPublishFile[0]);

        public PublishProjectOptions(IPublishFile[] additionalFiles = null, string destinationUrl = null) {
            _additionalFiles = additionalFiles ?? Default._additionalFiles;
            _destination = destinationUrl;
        }

        public IList<IPublishFile> AdditionalFiles {
            get {
                return _additionalFiles;
            }
        }

        /// <summary>
        /// Gets an URL which overrides the project publish settings or returns null if no override is specified.
        /// </summary>
        public string DestinationUrl {
            get {
                return _destination;
            }
        }
    }
}
