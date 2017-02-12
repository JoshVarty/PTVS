using System;
using System.Collections.Generic;
using Microsoft.PythonTools.Ipc.Json;

namespace Microsoft.PythonTools.TestAdapter {
    static class TestProtocol {
        public static readonly Dictionary<string, Type> RegisteredTypes = CollectCommands();

        private static Dictionary<string, Type> CollectCommands() {
            Dictionary<string, Type> all = new Dictionary<string, Type>();
            foreach (var type in typeof(TestProtocol).GetNestedTypes()) {
                if (type.IsSubclassOf(typeof(Request))) {
                    var command = type.GetField("Command");
                    if (command != null) {
                        all["request." + (string)command.GetRawConstantValue()] = type;
                    }
                } else if (type.IsSubclassOf(typeof(Event))) {
                    var name = type.GetField("Name");
                    if (name != null) {
                        all["event." + (string)name.GetRawConstantValue()] = type;
                    }
                }
            }
            return all;
        }

#pragma warning disable 0649

        public class StdOutEvent : Event {
            public const string Name = "stdout";
            public string content;

            public override string name => Name;
        }

        public class StdErrEvent : Event {
            public const string Name = "stderr";
            public string content;

            public override string name => Name;
        }

        public class StartEvent : Event {
            public const string Name = "start";
            public string test, file, method, classname;

            public override string name => Name;
        }

        public class ResultEvent : Event {
            public const string Name = "result";
            public string test, outcome, traceback, message;

            public override string name => Name;
        }

        public class DoneEvent : Event {
            public const string Name = "done";

            public override string name => Name;
        }

#pragma warning restore 0649
    }
}
