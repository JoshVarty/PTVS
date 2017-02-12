using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.PythonTools.Ipc.Json {
    public class Request {
        [JsonIgnore]
        public virtual string command => null;

        public override string ToString() => command;
    }

    public class GenericRequest : Request<Response> {
        public Dictionary<string, object> body;
    }

    public class Request<TResponse> : Request where TResponse : Response, new() {
    }
}