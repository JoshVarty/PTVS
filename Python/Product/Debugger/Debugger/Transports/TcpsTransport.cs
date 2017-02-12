using System;
using System.IO;
using System.Net.Security;
using System.Security.Authentication;
using System.Text;
using Microsoft.PythonTools.Infrastructure;

namespace Microsoft.PythonTools.Debugger.Transports {
    internal class TcpsTransport : TcpTransport {
        public override Stream Connect(Uri uri, bool requireAuthentication) {
            var rawStream = base.Connect(uri, requireAuthentication);
            try {
                var sslStream = new SslStream(rawStream, false, (sender, cert, chain, errs) => {
                    if (errs == SslPolicyErrors.None || !requireAuthentication) {
                        return true;
                    }

                    var errorDetails = new StringBuilder();
                    if ((errs & SslPolicyErrors.RemoteCertificateNotAvailable) != 0) {
                        errorDetails.AppendLine(Strings.DebugTcpsTransportConnectionErrorRemoteCertificateNotAvailable);
                    }
                    if ((errs & SslPolicyErrors.RemoteCertificateNameMismatch) != 0) {
                        errorDetails.AppendLine(Strings.DebugTcpsTransportConnectionErrorRemoteCertificateNameMismatch);
                    }
                    if ((errs & SslPolicyErrors.RemoteCertificateChainErrors) != 0) {
                        errorDetails.AppendLine(Strings.DebugTcpsTransportConnectionErrorRemoteCertificateChainErrors);
                    }

                    throw new AuthenticationException(Strings.DebugTcpsTransportConnectionError.FormatUI(uri, errorDetails));
                });

                sslStream.AuthenticateAsClient(uri.Host);
                rawStream = null;
                return sslStream;
            } catch (IOException ex) {
                throw new ConnectionException(ConnErrorMessages.RemoteNetworkError, ex);
            } finally {
                if (rawStream != null) {
                    rawStream.Dispose();
                }
            }
        }
    }
}
