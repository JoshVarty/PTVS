using System;
using Microsoft.VisualStudioTools;

namespace Microsoft.PythonTools.Commands {
    /// <summary>
    /// Provides the command for opening an arbitrary URL when selected.
    /// </summary>
    internal sealed class OpenWebUrlCommand : Command {
        private readonly IServiceProvider _serviceProvider;
        private readonly string _url;
        private readonly bool _useVSBrowser;

        public OpenWebUrlCommand(
            IServiceProvider serviceProvider,
            string url,
            uint commandId,
            bool useVSBrowser = true
        ) {
            _serviceProvider = serviceProvider;
            _url = url;
            _useVSBrowser = useVSBrowser;
            CommandId = (int)commandId;
        }

        public override void DoCommand(object sender, EventArgs args) {
            if (_useVSBrowser) {
                CommonPackage.OpenVsWebBrowser(_serviceProvider, _url);
            } else {
                CommonPackage.OpenWebBrowser(_url);
            }
        }

        public override int CommandId { get; }
    }
}
