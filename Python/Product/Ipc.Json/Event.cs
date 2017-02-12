using System.Collections.Generic;
using Newtonsoft.Json;

namespace Microsoft.PythonTools.Ipc.Json {
    public class Event {

        [JsonIgnore]
        public virtual string name => null;
    }


    public class GenericEvent : Event {
        public Dictionary<string, object> body;
    }
}

