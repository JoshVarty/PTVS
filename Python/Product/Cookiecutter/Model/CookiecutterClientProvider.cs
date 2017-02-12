using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CookiecutterTools.Infrastructure;
using Microsoft.CookiecutterTools.Interpreters;

namespace Microsoft.CookiecutterTools.Model {
    static class CookiecutterClientProvider {
        public static ICookiecutterClient Create(IServiceProvider provider, Redirector redirector) {
            var interpreter = FindCompatibleInterpreter();
            if (interpreter != null) {
                return new CookiecutterClient(provider, interpreter, redirector);
            }

            return null;
        }

        public static bool IsCompatiblePythonAvailable() {
            return FindCompatibleInterpreter() != null;
        }

        private static CookiecutterPythonInterpreter FindCompatibleInterpreter() {
            var interpreters = PythonRegistrySearch.PerformDefaultSearch();
            var compatible = interpreters
                .Where(x => File.Exists(x.Configuration.InterpreterPath))
                .OrderByDescending(x => x.Configuration.Version)
                .FirstOrDefault(x => x.Configuration.Version >= new Version(3, 3));
            return compatible != null ? new CookiecutterPythonInterpreter(compatible.Configuration.InterpreterPath) : null;
        }
    }
}
