namespace Microsoft.CookiecutterTools.Model {
    class DteCommand {
        public DteCommand(string name, string args) {
            Name = name;
            Args = args;
        }

        public string Name { get; }
        public string Args { get; }
    }
}
