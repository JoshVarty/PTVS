using System;
using System.IO;
using System.Web;
using Microsoft.VisualStudioTools;

namespace Microsoft.PythonTools.Debugger {
    public class WebSocketProxy : WebSocketProxyBase {
        public override int DebuggerPort {
            get { return 5678; }
        }

        public override bool AllowConcurrentConnections {
            // We don't actually support more than one debugger connection, but the debugger will take care of rejecting the extra ones.
            // We do however use two different connections for debugger and debug REPL, so this must be true.
            get { return true; }
        }

        public override void ProcessHelpPageRequest(HttpContext context) {
            using (var stream = GetType().Assembly.GetManifestResourceStream("Microsoft.PythonTools.WebRole.WebSocketProxy.html"))
            using (var reader = new StreamReader(stream)) {
                string html = reader.ReadToEnd();
                var wsUri = new UriBuilder(context.Request.Url) { Scheme = "wss", Port = -1, UserName = "secret" }.ToString();
                wsUri = wsUri.Replace("secret@", "<span class='secret'>secret</span>@");
                context.Response.Write(html.Replace("{{WS_URI}}", wsUri));
                context.Response.End();
            }
        }
    }
}

