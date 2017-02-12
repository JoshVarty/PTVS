namespace Microsoft.PythonTools.Parsing {
    sealed class Ellipsis {
        public static readonly Ellipsis Value = new Ellipsis();

        public override string ToString() {
            return "...";
        }
    }
}
