using Microsoft.VisualStudio.Text;

namespace Microsoft.PythonTools.Intellisense {
    /// <summary>
    /// Wraps up a response which includes versioned response information.  This includes
    /// the data its self as well as a LocationTracker.
    /// 
    /// This is used to capture the last analysis version before we send a request
    /// to the out of proc server.  This ensures that we have not yet received the
    /// OnNewAnalysis event and that we will be able to track changes through whatever
    /// response version we get from the remote side.
    /// </summary>
    sealed class VersionedResponse<T> {
        public readonly T Data;
        private readonly ITextBuffer _buffer;
        private readonly ITextVersion _versionBeforeRequest;

        public VersionedResponse(T data, ITextBuffer buffer, ITextVersion versionBeforeRequest) {
            Data = data;
            _buffer = buffer;
            _versionBeforeRequest = versionBeforeRequest;
        }

        public LocationTracker GetTracker(int fromVersion) {
            if (fromVersion >= _versionBeforeRequest.VersionNumber) {
                return new LocationTracker(
                    _versionBeforeRequest,
                    _buffer,
                    fromVersion
                );
            }
            return null;
        }
    }
}
