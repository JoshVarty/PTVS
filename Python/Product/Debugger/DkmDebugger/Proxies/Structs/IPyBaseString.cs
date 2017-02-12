namespace Microsoft.PythonTools.DkmDebugger.Proxies.Structs {
    internal interface IPyBaseStringObject : IPyObject {
    }

    internal static class PyBaseStringExtensions {
        public static string ToStringOrNull(this IPyBaseStringObject s) {
            return s == null ? null : s.ToString();
        }
    }
}
