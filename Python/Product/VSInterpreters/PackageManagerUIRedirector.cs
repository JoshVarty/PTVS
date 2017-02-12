using System;
using Microsoft.PythonTools.Infrastructure;

namespace Microsoft.PythonTools.Interpreter {
    class PackageManagerUIRedirector : Redirector {
        private readonly IPackageManager _sender;
        private readonly IPackageManagerUI _ui;

        public static Redirector Get(IPackageManager sender, IPackageManagerUI ui) {
            if (ui != null) {
                return new PackageManagerUIRedirector(sender, ui);
            }
            return null;
        }

        private PackageManagerUIRedirector(IPackageManager sender, IPackageManagerUI ui) {
            _sender = sender;
            _ui = ui;
        }

        public override void WriteErrorLine(string line) {
            _ui.OnErrorTextReceived(_sender, line + Environment.NewLine);
        }

        public override void WriteLine(string line) {
            _ui.OnOutputTextReceived(_sender, line + Environment.NewLine);
        }
    }
}
