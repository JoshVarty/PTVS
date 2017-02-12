
namespace Microsoft.PythonTools.Ipc.Json {
    public sealed class RequestArgs {
        private readonly string _command;
        private readonly Request _request;

        public RequestArgs(string command, Request request) {
            _command = command;
            _request = request;
        }

        public Request Request => _request;
        public string Command => _command;
    }
}
