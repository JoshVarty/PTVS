using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.CookiecutterTools.Infrastructure {
    static partial class NativeMethods {
        [DllImport("kernel32", EntryPoint = "GetBinaryTypeW", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Winapi)]
        private static extern bool _GetBinaryType(string lpApplicationName, out GetBinaryTypeResult lpBinaryType);

        private enum GetBinaryTypeResult : uint {
            SCS_32BIT_BINARY = 0,
            SCS_DOS_BINARY = 1,
            SCS_WOW_BINARY = 2,
            SCS_PIF_BINARY = 3,
            SCS_POSIX_BINARY = 4,
            SCS_OS216_BINARY = 5,
            SCS_64BIT_BINARY = 6
        }

        public static ProcessorArchitecture GetBinaryType(string path) {
            GetBinaryTypeResult result;

            if (_GetBinaryType(path, out result)) {
                switch (result) {
                    case GetBinaryTypeResult.SCS_32BIT_BINARY:
                        return ProcessorArchitecture.X86;
                    case GetBinaryTypeResult.SCS_64BIT_BINARY:
                        return ProcessorArchitecture.Amd64;
                    case GetBinaryTypeResult.SCS_DOS_BINARY:
                    case GetBinaryTypeResult.SCS_WOW_BINARY:
                    case GetBinaryTypeResult.SCS_PIF_BINARY:
                    case GetBinaryTypeResult.SCS_POSIX_BINARY:
                    case GetBinaryTypeResult.SCS_OS216_BINARY:
                    default:
                        break;
                }
            }

            return ProcessorArchitecture.None;
        }

        public const int MAX_PATH = 260; // windef.h	
        public const int MAX_FOLDER_PATH = MAX_PATH - 12;   // folders need to allow 8.3 filenames, so MAX_PATH - 12

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern uint GetFinalPathNameByHandle(
            SafeHandle hFile,
            [Out]StringBuilder lpszFilePath,
            uint cchFilePath,
            uint dwFlags
        );

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern SafeFileHandle CreateFile(
            string lpFileName,
            FileDesiredAccess dwDesiredAccess,
            FileShareFlags dwShareMode,
            IntPtr lpSecurityAttributes,
            FileCreationDisposition dwCreationDisposition,
            FileFlagsAndAttributes dwFlagsAndAttributes,
            IntPtr hTemplateFile
        );

        [Flags]
        public enum FileDesiredAccess : uint {
            FILE_LIST_DIRECTORY = 1
        }

        [Flags]
        public enum FileShareFlags : uint {
            FILE_SHARE_READ = 0x00000001,
            FILE_SHARE_WRITE = 0x00000002,
            FILE_SHARE_DELETE = 0x00000004
        }

        [Flags]
        public enum FileCreationDisposition : uint {
            OPEN_EXISTING = 3
        }

        [Flags]
        public enum FileFlagsAndAttributes : uint {
            FILE_FLAG_BACKUP_SEMANTICS = 0x02000000
        }

        public static IntPtr INVALID_FILE_HANDLE = new IntPtr(-1);
        public static IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);
    }
}
