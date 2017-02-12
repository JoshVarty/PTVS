using System;
using System.IO;
using Microsoft.PythonTools.Debugger;
using TestUtilities;

namespace DebuggerTests {
    static class DebugExtensions {
        internal static PythonProcess DebugProcess(this PythonDebugger debugger, PythonVersion version, string filename, Action<PythonProcess, PythonThread> onLoaded = null, bool resumeOnProcessLoaded = true, string interpreterOptions = null, PythonDebugOptions debugOptions = PythonDebugOptions.RedirectOutput, string cwd = null, string arguments = "") {
            string fullPath = Path.GetFullPath(filename);
            string dir = cwd ?? Path.GetFullPath(Path.GetDirectoryName(filename));
            if (!String.IsNullOrEmpty(arguments)) {
                arguments = "\"" + fullPath + "\" " + arguments;
            } else {
                arguments = "\"" + fullPath + "\"";
            }
            var process = debugger.CreateProcess(version.Version, version.InterpreterPath, arguments, dir, "", interpreterOptions, debugOptions);
            process.DebuggerOutput += (sender, args) => {
                Console.WriteLine("{0}: {1}", args.Thread.Id, args.Output);
            };
            process.ProcessLoaded += (sender, args) => {
                if (onLoaded != null) {
                    onLoaded(process, args.Thread);
                }
                if (resumeOnProcessLoaded) {
                    process.Resume();
                }
            };

            return process;
        }

        internal static PythonBreakpoint AddBreakPointByFileExtension(this PythonProcess newproc, int line, string finalBreakFilename) {
            PythonBreakpoint breakPoint;
            var ext = Path.GetExtension(finalBreakFilename);

            if (String.Equals(ext, ".html", StringComparison.OrdinalIgnoreCase) ||
                String.Equals(ext, ".htm", StringComparison.OrdinalIgnoreCase) ||
                String.Equals(ext, ".djt", StringComparison.OrdinalIgnoreCase)) {
                breakPoint = newproc.AddDjangoBreakPoint(finalBreakFilename, line);
            } else {
                breakPoint = newproc.AddBreakPoint(finalBreakFilename, line);
            }
            return breakPoint;
        }


    }
}
