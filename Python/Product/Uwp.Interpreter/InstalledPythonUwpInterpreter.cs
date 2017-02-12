using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Microsoft.PythonTools.Uwp.Interpreter {
    internal class InstalledPythonUwpInterpreter {
        static public string GetDirectory(Version ver) {
            string result;
            if (_installedSdks.Value.TryGetValue(ver, out result)) {
                return result;
            }
            return null;
        }

        static public IEnumerable<KeyValuePair<Version, string>> GetInterpreters() {
            return _installedSdks.Value;
        }

        static private Lazy<Dictionary<Version, String>> _installedSdks = new Lazy<Dictionary<Version, string>>(DiscoverPythonUwpSdks, LazyThreadSafetyMode.ExecutionAndPublication);

        static private Dictionary<Version, String> DiscoverPythonUwpSdks() {
            var userSdkInstallDir = Environment.ExpandEnvironmentVariables(@"%LOCALAPPDATA%\Microsoft\VisualStudio\%VISUALSTUDIOVERSION%Exp\Extensions\Microsoft\Python UWP");
            var sdkInstallDir = Environment.ExpandEnvironmentVariables(@"%ProgramFiles(x86)%\Microsoft SDKs\Windows\v10.0\ExtensionSDKs\Python UWP");

            return FindPythonSdkFromDirectories(sdkInstallDir, userSdkInstallDir);
        }

        static private Dictionary<Version, string> FindPythonSdkFromDirectories(params string[] directories) {
            var pythonSdkMap = new Dictionary<Version, string>();

            try {
                if (directories != null) {
                    for (int i = 0; i < directories.Length; i++) {
                        var rootDirectoryInfo = new DirectoryInfo(directories[i]);

                        if (rootDirectoryInfo.Exists) {
                            foreach (var dirInfo in rootDirectoryInfo.EnumerateDirectories()) {
                                Version pythonUwpVersion;

                                if (Version.TryParse(dirInfo.Name, out pythonUwpVersion)) {
                                    var prefixPath = dirInfo.GetDirectories(PythonUwpConstants.InterpreterRelativePath).FirstOrDefault();
                                    if (prefixPath != null && prefixPath.Exists) {
                                        var targetsFile = prefixPath?.GetFiles(PythonUwpConstants.InterpreterFile).FirstOrDefault();
                                        var libPath = prefixPath?.GetDirectories(PythonUwpConstants.InterpreterLibPath).FirstOrDefault();

                                        if (targetsFile != null && libPath != null) {
                                            // Note, the order of discovery is important.  The last one wins.
                                            pythonSdkMap[pythonUwpVersion] = prefixPath.FullName;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            } catch (IOException) {
                // IOException is not critical here, just means we cannot interrogate for factories at this point
            }

            return pythonSdkMap;
        }
    }
}
