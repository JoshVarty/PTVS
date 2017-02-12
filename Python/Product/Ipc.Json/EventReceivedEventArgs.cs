using System;

namespace Microsoft.PythonTools.Ipc.Json {
    public sealed class EventReceivedEventArgs : EventArgs {
        private readonly string _name;
        private readonly Event _event;

        public EventReceivedEventArgs(string name, Event event_) {
            _name = name;
            _event = event_;
        }

        public Event Event => _event;
        public string Name => _name;
    }
}

