
namespace Microsoft.PythonTools.Analysis.Values {
    class SliceInfo : AnalysisValue {
        /*private IAnalysisSet _start;
        private IAnalysisSet _stop;
        private IAnalysisSet _step;*/
        public static SliceInfo Instance = new SliceInfo();

        public SliceInfo() { }
        /*
        public SliceInfo(IAnalysisSet start, IAnalysisSet stop, IAnalysisSet step) {
            _start = start;
            _stop = stop;
            _step = step;
        }
        */
    }
}
