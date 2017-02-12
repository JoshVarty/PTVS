
namespace Microsoft.PythonTools.Intellisense {
    struct ExtractMethodResult {
        public readonly string Method;
        public readonly string Call;

        public ExtractMethodResult(string newMethod, string newCall) {
            Method = newMethod;
            Call = newCall;
        }
    }
}
