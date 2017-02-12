using System;
using System.Runtime.InteropServices;

namespace Microsoft.PythonTools.Profiling {
    [Guid("20F87722-745A-48C7-B9D5-DD9B85F96B7F")]
    public interface IPythonProfileSession {
        string Name {
            get;
        }

        string Filename {
            get;
        }

        IPythonPerformanceReport GetReport(object item);

        void Save(string filename = null);

        void Launch(bool openReport = false);

        bool IsSaved {
            get;
        }
    }
}
