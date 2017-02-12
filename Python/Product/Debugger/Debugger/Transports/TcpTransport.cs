using System;
using System.IO;
using System.Net.Sockets;

namespace Microsoft.PythonTools.Debugger.Transports {
    internal class TcpTransport : IDebuggerTransport {
        public const ushort DefaultPort = 5678;

        public Exception Validate(Uri uri) {
            if (uri.AbsolutePath != "/") {
                return new FormatException(Strings.DebugTcpTransportUriCannotContainPath);
            }
            return null;
        }

        public virtual Stream Connect(Uri uri, bool requireAuthentication) {
            if (uri.Port < 0) {
                uri = new UriBuilder(uri) { Port = DefaultPort }.Uri;
            }

            // PTVSD is using AF_INET by default, so lets make sure to try the IPv4 address in lieu of IPv6 address
            var tcpClient = new TcpClient(AddressFamily.InterNetwork);
            
            try {
                tcpClient.Connect(uri.Host, uri.Port);
                var stream = tcpClient.GetStream();
                tcpClient = null;
                return stream;
            } catch (IOException ex) {
                throw new ConnectionException(ConnErrorMessages.RemoteNetworkError, ex);
            } finally {
                if (tcpClient != null) {
                    tcpClient.Close();
                }
            }
        }
    }
}
