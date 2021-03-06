using System;
using System.IO;
using System.Reflection;
using Microsoft.Win32;

namespace Microsoft.IronPythonTools.Interpreter {
    internal class IronPythonResolver {
        public static Assembly domain_AssemblyResolve(object sender, ResolveEventArgs args) {
            var pythonInstallDir = GetPythonInstallDir();
            var asmName = new AssemblyName(args.Name);
            var asmPath = Path.Combine(pythonInstallDir, asmName.Name + ".dll");
            if (File.Exists(asmPath)) {
                return Assembly.LoadFile(asmPath);
            }
            return null;
        }

        internal static string GetPythonInstallDir() {
            using (var ipy = Registry.LocalMachine.OpenSubKey("SOFTWARE\\IronPython")) {
                if (ipy != null) {
                    using (var twoSeven = ipy.OpenSubKey("2.7")) {
                        if (twoSeven != null) {
                            var installPath = twoSeven.OpenSubKey("InstallPath");
                            if (installPath != null) {
                                var res = installPath.GetValue("") as string;
                                if (res != null) {
                                    return res;
                                }
                            }
                        }
                    }
                }
            }

            var paths = Environment.GetEnvironmentVariable("PATH");
            if (paths != null) {
                foreach (string dir in paths.Split(Path.PathSeparator)) {
                    try {
                        if (IronPythonExistsIn(dir)) {
                            return dir;
                        }
                    } catch {
                        // ignore
                    }
                }
            }

            return null;
        }

        private static bool IronPythonExistsIn(string/*!*/ dir) {
            return File.Exists(Path.Combine(dir, "ipy.exe"));
        }
    }
}
