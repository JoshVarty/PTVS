using System;
using System.Diagnostics;
using System.Threading;

namespace Microsoft.PythonTools.Infrastructure {
    public static class CancellationTokens {
        private static CancellationToken GetToken(TimeSpan delay) {
            if (Debugger.IsAttached) {
                return CancellationToken.None;
            }
            return new CancellationTokenSource(delay).Token;
        }

        public static CancellationToken After60s => GetToken(TimeSpan.FromSeconds(60));
        public static CancellationToken After15s => GetToken(TimeSpan.FromSeconds(15));
        public static CancellationToken After5s => GetToken(TimeSpan.FromSeconds(5));
        public static CancellationToken After1s => GetToken(TimeSpan.FromSeconds(1));
        public static CancellationToken After500ms => GetToken(TimeSpan.FromMilliseconds(500));
        public static CancellationToken After100ms => GetToken(TimeSpan.FromMilliseconds(100));
    }
}
