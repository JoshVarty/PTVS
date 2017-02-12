using System;
using System.Runtime.InteropServices;

namespace Microsoft.PythonTools.Profiling {
    [Guid("042A8DBE-A800-40CF-8CE2-903E2746C109")]
    public interface IPythonPerformanceReport {
        string Filename {
            get;
        }
    }
}
