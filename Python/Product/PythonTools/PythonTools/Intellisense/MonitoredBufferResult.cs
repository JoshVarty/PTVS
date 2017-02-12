namespace Microsoft.PythonTools.Intellisense {
    struct MonitoredBufferResult {
        public readonly BufferParser BufferParser;
        public readonly AnalysisEntry AnalysisEntry;

        public MonitoredBufferResult(BufferParser bufferParser, AnalysisEntry projectEntry) {
            BufferParser = bufferParser;
            AnalysisEntry = projectEntry;
        }
    }
}
