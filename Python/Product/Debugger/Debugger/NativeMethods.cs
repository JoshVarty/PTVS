using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.PythonTools.Debugger {
    internal static class NativeMethods {
        [DllImport("kernel32", SetLastError = true, ExactSpelling = true)]
        public static extern Int32 WaitForSingleObject(SafeWaitHandle handle, Int32 milliseconds);
    }
}
