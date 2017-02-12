using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.PythonTools.Interpreter;

namespace TestUtilities.Python {
    class TestPackageManagerUI : IPackageManagerUI {
        private static string RemoveNewline(string text) {
            if (string.IsNullOrEmpty(text)) {
                return string.Empty;
            }

            if (text[text.Length - 1] == '\n') {
                if (text.Length >= 2 && text[text.Length - 2] == '\r') {
                    return text.Remove(text.Length - 2);
                }
                return text.Remove(text.Length - 1);
            }
            return text;
        }

        public void OnOutputTextReceived(IPackageManager sender, string text) {
            Trace.TraceInformation(RemoveNewline(text));
        }

        public void OnErrorTextReceived(IPackageManager sender, string text) {
            Trace.TraceError(RemoveNewline(text));
        }

        public void OnOperationFinished(IPackageManager sender, string operation, bool success) {
            Trace.TraceInformation("{0} finished. Success: {1}", operation, success);
        }

        public void OnOperationStarted(IPackageManager sender, string operation) {
            Trace.TraceInformation("{0} started.", operation);
        }

        public Task<bool> ShouldElevateAsync(IPackageManager sender, string operation) {
            return Task.FromResult(false);
        }
    }
}
