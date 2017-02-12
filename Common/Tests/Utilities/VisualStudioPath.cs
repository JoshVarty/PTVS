using System;
using System.IO;
using Microsoft.Win32;

namespace TestUtilities {
    public static class VisualStudioPath {
        private static string _root = GetRootPath();

        private static string GetRootPath() {
            string vsDir = null;
            using (var baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32))
            using (var key = baseKey.OpenSubKey("SOFTWARE\\Microsoft\\VisualStudio\\SxS\\VS7")) {
                if (key != null) {
                    vsDir = key.GetValue(AssemblyVersionInfo.VSVersion) as string;
                }
            }

            return vsDir;
        }

        public static string Root {
            get {
                if (!Directory.Exists(_root)) {
                    throw new InvalidOperationException("Cannot find VS installation");
                }
                return _root;
            }
        }

        public static string PublicAssemblies {
            get {
                return Path.Combine(Root, "Common7", "IDE", "PublicAssemblies");
            }
        }

        public static string PrivateAssemblies {
            get {
                return Path.Combine(Root, "Common7", "IDE", "PrivateAssemblies");
            }
        }
    }
}
