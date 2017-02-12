namespace Microsoft.CookiecutterTools.Model {
    class ContextItem {
        public ContextItem(string name, string selector, string defaultValue, string[] items = null) {
            Name = name;
            Selector = selector;
            DefaultValue = defaultValue;
            Values = items ?? new string[0];
        }

        public string Name { get; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Selector { get; set; }
        public string DefaultValue { get; }
        public string[] Values { get; }
    }
}
