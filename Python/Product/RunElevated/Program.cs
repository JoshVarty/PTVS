using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.PythonTools.Infrastructure {
    sealed class Program {
        public static int Main(string[] args) {
            int port;
            try {
                port = int.Parse(args[0]);
            } catch (IndexOutOfRangeException) {
                Console.WriteLine("Port number expected");
                return 1;
            }

            try {
                return Run(port).WaitAndUnwrapExceptions();
            } catch (OperationCanceledException) {
                return 2;
            } catch (IOException) {
                return 255;
            } catch (Exception ex) {
                Debug.Fail(ex.ToUnhandledExceptionMessage(typeof(Program)));
                return 255;
            }
        }

        private static async Task<int> Run(int port) {
            using (var client = new TcpClient()) {
                await client.ConnectAsync(IPAddress.Loopback, port);

                var utf8 = new UTF8Encoding(false);
                using (var reader = new StreamReader(client.GetStream(), utf8, false, 4096, true))
                using (var writer = new StreamWriter(client.GetStream(), utf8, 4096, true)) {
                    var filename = await reader.ReadLineAsync();
                    var args = (await reader.ReadLineAsync()).Split('|')
                        .Select(s => utf8.GetString(Convert.FromBase64String(s)))
                        .ToList();
                    var workingDir = await reader.ReadLineAsync();
                    var env = (await reader.ReadLineAsync()).Split('|')
                        .Select(s => s.Split(new[] { '=' }, 2))
                        .Select(s => new KeyValuePair<string, string>(s[0], utf8.GetString(Convert.FromBase64String(s[1]))))
                        .ToList();
                    var outputEncoding = await reader.ReadLineAsync();
                    var errorEncoding = await reader.ReadLineAsync();

                    return await ProcessOutput.Run(
                        filename,
                        args,
                        workingDir,
                        env,
                        false,
                        new StreamRedirector(writer, outputPrefix: "OUT:", errorPrefix: "ERR:"),
                        quoteArgs: false,
                        elevate: false,
                        outputEncoding: string.IsNullOrEmpty(outputEncoding) ? null : Encoding.GetEncoding(outputEncoding),
                        errorEncoding: string.IsNullOrEmpty(errorEncoding) ? null : Encoding.GetEncoding(errorEncoding)
                    );
                }
            }
        }
    }
}
