
namespace Microsoft.VisualStudioTools.Project {
    class PublishFile : IPublishFile {
        private readonly string _filename, _destFile;

        public PublishFile(string filename, string destFile) {
            _filename = filename;
            _destFile = destFile;
        }

        #region IPublishFile Members

        public string SourceFile {
            get { return _filename; }
        }

        public string DestinationFile {
            get { return _destFile; }
        }

        #endregion
    }
}
